using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Model;
using suaconsulta_api.Services;
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
        public IActionResult SignUp([FromServices] JwtService jwtService, [FromBody] SignUpDto dto)
        {
            return getServiceController<InterfaceAuthService>().DoSignUp(dto);
        }

        [HttpPost]
        [Route("Login/")]
        public IActionResult Login([FromServices] JwtService jwtService,[FromBody] LoginRequest request)
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
        public IActionResult TokenInformation(Boolean justUser)
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
