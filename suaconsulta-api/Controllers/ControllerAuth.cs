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
        public async Task<IActionResult> SignUp([FromBody] SignUpDto dto)
        {
            return await getServiceController<InterfaceAuthService>().DoSignUp(dto);
        }

        [HttpPost]
        [Route("Login/")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return await getServiceController<InterfaceAuthService>().DoLogin(request);
        }

        [HttpGet]
        [Authorize]
        [Route("tokenInformation/")]
        public async Task<IActionResult> TokenInformation(Boolean justUser)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado");
            }

            if (justUser)
            {
                var user = await getRepositoryController<userRepository>().getUserById(int.Parse(userId));
                if (user == null)
                {
                    return NotFound("Usuário não encontrado");
                }
                return Ok(user);
            }

            var userExternalInfo = await getRepositoryController<userRepository>().getExternalUserInfo(int.Parse(userId));
            if (userExternalInfo == null)
                return NotFound("Informações externas do usuário não encontradas");

            return Ok(userExternalInfo);
        }
    }
}
