using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Infrastructure.Data;
using suaconsulta_api.Infrastructure.Repositories;

namespace suaconsulta_api.Domain.Services
{
    public class UserService
    {
        private readonly InterfaceUserRepository _userRepository;

        public UserService(InterfaceUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Método serve para relacionar um external Id ao usuário logado.
        /// Por exemplo na criação de um usuário paciente, este método relaciona o Id do paciente com o usuário.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="externalId"></param>
        public void RelateExternalId(int userId, int externalId)
        {
            _userRepository.setExternalId(userId, externalId);
        }
    }
}