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
    public class ControllerAuth : ControllerApiBase
    {

        public ControllerAuth(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromServices] AppDbContext context, [FromServices] JwtService jwtService, [FromBody] SignUpDto dto)
        {
            if (getServiceController<InterfaceAuthService>().isEmailAlreadyRegister(dto).Result)
            {
                return Conflict("Email já cadastrado");
            }

            if (!getServiceController<InterfaceAuthService>().isPasswordValid(dto.pass).Result)
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
            var user = getRepositoryController<InterfaceAuthRepository>().getUserByEmail(request.Email).Result;
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
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado");
            }

            var user = getRepositoryController<userRepository>().getUserById(int.Parse(userId)).Result;
            if (user == null)
            {
                return Unauthorized("Usuário não encontrado");
            }

            if (justUser)
                return Ok(user);
            
            var userExternalInfo = getRepositoryController<userRepository>().getExternalUserInfo(user.Id).Result;
            if (userExternalInfo == null)
                return NotFound("Informações externas do usuário não encontradas");
                
            return Ok(userExternalInfo);
        }
    }
}
