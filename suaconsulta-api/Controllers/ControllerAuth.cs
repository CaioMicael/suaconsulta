using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Data;
using suaconsulta_api.Model;
using suaconsulta_api.Services;
using suaconsulta_api.Model.Enum;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using suaconsulta_api.DTO;

namespace suaconsulta_api.Controllers
{
    [Route("api/Auth/")]
    public class ControllerAuth : ControllerBase
    {
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromServices] AppDbContext context, [FromServices] JwtService jwtService, [FromBody] SignUpDto dto)
        {
            // Verifica se o email já está cadastrado
            var existingUser = context.Users.FirstOrDefault(u => u.Mail == dto.mail);
            if (existingUser != null)
            {
                return Conflict("Email já cadastrado");
            }

            if (dto.pass == null || dto.pass.Length < 6)
            {
                return BadRequest("Senha deve ter pelo menos 6 caracteres");
            }

            var hasher = new PasswordHasher<ModelUsers>();
            string hash = hasher.HashPassword(null, dto.pass);
            var user = new ModelUsers
            {
                ExternalId = 0, // Defina o valor adequado para ExternalId
                TypeUser = dto.TypeUser,
                Mail = dto.mail,
                Password = hash
            };
            context.Users.Add(user);
            context.SaveChanges();
            // Gera o JWT
            var token = jwtService.GenerateToken(user);
            return Ok(new { token = token, role = user.TypeUser });
        }

        [HttpPost]
        [Route("Login/")]
        public IActionResult Login([FromServices] AppDbContext context, [FromServices] JwtService jwtService,[FromBody] LoginRequest request)
        {
            var user = context.Users.FirstOrDefault(u => u.Mail == request.Email);
            if (user == null)
            {
                return Unauthorized("Usuário não encontrado");
            }

            var hasher = new PasswordHasher<ModelUsers>();
            var result = hasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (result != PasswordVerificationResult.Success)
                return Unauthorized("Senha incorreta");

            // Gera o JWT
            var token = jwtService.GenerateToken(user);

            return Ok(new { token = token, role = user.TypeUser });
        }

        [HttpPost]
        [Authorize]
        [Route("tokenInformation/")]
        public IActionResult TokenInformation([FromServices] JwtService jwtService, [FromServices] AppDbContext context, Boolean justUser)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = context.Users.FirstOrDefault(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return Unauthorized("Usuário não encontrado");
            }

            if (justUser)
                return Ok(user);
            
            if (user.TypeUser == EnumTypeUsers.Patient)
            {
                var response = context.Users.
                    Join(context.Patient, user => user.ExternalId, patient => patient.Id, (user, patient) => new
                    {
                        user,
                        patient
                    } ).
                    FirstOrDefault(U => U.patient.Id == user.Id);
                if (response != null)
                    return Ok(response);
            }
            else if (user.TypeUser == EnumTypeUsers.Doctor)
            {
                var  response = context.Users.
                    Join(context.Doctor, user => user.ExternalId, doctor => doctor.Id, (user, doctor) => new
                    {
                        user,
                        doctor
                    }).
                    FirstOrDefault(U => U.doctor.Id == user.Id);
                if (response != null)
                    return Ok(response);
            }

            return NotFound("Usuário não encontrado");
        }

        /// <summary>
        /// Método estático serve para relacionar um external Id ao usuário logado.
        /// Por exemplo na criação de um usuário paciente, este método relaciona o Id do paciente com o usuário.
        /// </summary>
        /// <param name="externalId"></param>
        public static void RelateExternalId([FromServices] AppDbContext context, string userId, int externalId)
        {
            var user = context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

            if (user != null)
            {
                if (user.ExternalId == null || user.ExternalId == 0)
                {
                    user.ExternalId = externalId;
                    context.SaveChanges();
                }
                else
                    throw new Exception("Usuário já possui um cadastro vinculado");
            }
            else
            {
                throw new Exception("Usuário não encontrado");
            }
        }
    }
}
