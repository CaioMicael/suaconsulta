using Microsoft.AspNetCore.Mvc;
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
        public IActionResult SignUp([FromBody] SignUpDto dto)
        {
            return getServiceController<InterfaceAuthService>().DoSignUp(dto);
        }

        [HttpPost]
        [Route("Login/")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            return getServiceController<InterfaceAuthService>().DoLogin(request);
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

            if (justUser)
            {
                var user = getRepositoryController<userRepository>().getUserById(int.Parse(userId)).Result;
                if (user == null)
                {
                    return Unauthorized("Usuário não encontrado");
                }
                
                return Ok(user);
            }
            
            var userExternalInfo = getRepositoryController<userRepository>().getExternalUserInfo(int.Parse(userId)).Result;
            if (userExternalInfo == null)
                return NotFound("Informações externas do usuário não encontradas");

            return Ok(userExternalInfo);
        }
    }
}
