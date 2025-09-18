using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using suaconsulta_api.Domain.Services;
using suaconsulta_api.Application.DTO;
using suaconsulta_api.Infrastructure.Repositories;
using suaconsulta_api.Core.Common;

namespace suaconsulta_api.Application.Controllers
{
    [Route("api/Auth/")]
    public class ControllerAuth : ControllerBase
    {
        private readonly InterfaceAuthService _authService;
        private readonly InterfaceUserRepository _userRepository;

        public ControllerAuth(InterfaceAuthService authService, InterfaceUserRepository userRepository)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<Result<TokenInformationDto>> SignUp([FromBody] SignUpDto dto)
        {
            return await _authService.DoSignUp(dto);
        }

        [HttpPost]
        [Route("Login/")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Os campos enviados não estão corretos");
            }
            return await _authService.DoLogin(request);
        }

        [HttpGet]
        [Authorize]
        [Route("tokenInformation/")]
        public async Task<IActionResult> TokenInformation(bool justUser)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado");
            }

            if (justUser)
            {
                var user = await _userRepository.getUserById(int.Parse(userId));
                if (user == null)
                {
                    return NotFound("Usuário não encontrado");
                }
                return Ok(user);
            }

            var userExternalInfo = await _userRepository.getExternalUserInfo(int.Parse(userId));
            if (userExternalInfo == null)
                return NotFound("Informações externas do usuário não encontradas");

            return Ok(userExternalInfo);
        }
    }
}
