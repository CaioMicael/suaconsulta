using Microsoft.AspNetCore.Mvc;

namespace suaconsulta_api.Controllers
{
    /// <summary>
    /// Base controller class for API controllers.
    /// Provê injeções de dependência para serviços e repositórios.
    /// </summary>
    public class ControllerApiBase : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public ControllerApiBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Obtém uma instância do serviço especificado.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T getServiceController<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        /// <summary>
        /// Obtém uma instância do repositório especificado.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T getRepositoryController<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}