using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Infrastructure.Repositories;

namespace suaconsulta_api.Application.Controllers
{
    /// <summary>
    /// Base controller class for API controllers.
    /// Preve injeções de dependência para serviços e repositórios comuns entre os controllers.
    /// </summary>
    public class ControllerApiBase : ControllerBase
    {
        private readonly userRepository _userRepository;

        public ControllerApiBase(userRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
    }
}