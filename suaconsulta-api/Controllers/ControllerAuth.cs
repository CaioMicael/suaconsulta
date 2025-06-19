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

namespace suaconsulta_api.Controllers
{
    [Route("api/Auth/")]
    public class ControllerAuth : ControllerBase
    {
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromServices] AppDbContext context, [FromServices] JwtService jwtService, string Mail, string Pass, EnumTypeUsers TypeUser)
        {
            var hasher = new PasswordHasher<ModelUsers>();
            string hash = hasher.HashPassword(null, Pass);
            var user = new ModelUsers
            {
                ExternalId = 0, // Defina o valor adequado para ExternalId
                TypeUser = TypeUser,
                Mail = Mail,
                Password = hash
            };
            context.Users.Add(user);
            context.SaveChanges();
            return CreatedAtAction(nameof(SignUp), new { id = user.Id }, user);
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
        public IActionResult TokenInformation([FromServices] JwtService jwtService, [FromServices] AppDbContext context)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = context.Users.FirstOrDefault(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return Unauthorized("Usuário não encontrado");
            }
            
            if (user.TypeUser == EnumTypeUsers.Patient)
            {
                return Ok(context.Users.
                    Join(context.Patient, U => U.ExternalId, P => P.Id, (U, P) => new
                    {
                        U,
                        P
                    } ).
                    FirstOrDefault(U => U.U.Id == user.Id));
            }

            if (user.TypeUser == EnumTypeUsers.Doctor)
            {
                return Ok(context.Users.
                    Join(context.Doctor, U => U.ExternalId, D => D.Id, (U, D) => new
                    {
                        U,
                        D
                    }).
                    FirstOrDefault(U => U.U.Id == user.Id));
            }

            return NotFound("Usuário não encontrado");
        }
    }
}
