using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Data;
using suaconsulta_api.Model;
using suaconsulta_api.Services;
using suaconsulta_api.Model.Enum;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using suaconsulta_api.DTO;
using Microsoft.AspNetCore.Identity.Data;
using suaconsulta_api.Repositories;

namespace suaconsulta_api.Controllers
{
    [Route("api/Auth/")]
    public class ControllerAuth : ControllerBase
    {

        private readonly AuthService authService;

        private readonly AuthRepository authRepository;

        public ControllerAuth(AuthService authService)
        {
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));
            this.authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromServices] AppDbContext context, [FromServices] JwtService jwtService, [FromBody] SignUpDto dto)
        {
            if (authService.isEmailAlreadyRegister(dto).Result)
            {
                return Conflict("Email já cadastrado");
            }

            if (!authService.isPasswordValid(dto.pass).Result)
            {
                return BadRequest("Senha deve ter pelo menos 6 caracteres");
            }

            var hasher = new PasswordHasher<ModelUsers>();
            string hash = hasher.HashPassword(null, dto.pass);
            var user = new ModelUsers
            {
                ExternalId = 0,
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
            
            var user = authRepository.getUserByEmail(request.Email).Result;
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

        [HttpGet]
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
                    Join(context.Patient, u => u.ExternalId, patient => patient.Id, (u, patient) => new
                    {
                        user = u,
                        patient
                    }).
                    FirstOrDefault(result => result.user.Id == user.Id);
                if (response != null)
                    return Ok(response);
            }
            else if (user.TypeUser == EnumTypeUsers.Doctor)
            {
                var  response = context.Users.
                    Join(context.Doctor, u => u.ExternalId, doctor => doctor.Id, (u, doctor) => new
                    {
                        user = u,
                        doctor
                    }).
                    FirstOrDefault(result => result.user.Id == user.Id);
                if (response != null)
                    return Ok(response);
            }

            return NotFound("Usuário não encontrado");
        }
    }
}
