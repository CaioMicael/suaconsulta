using suaconsulta_api.DTO;
using suaconsulta_api.Model;

namespace suaconsulta_api.Services
{
    public interface InterfaceAuthService
    {
        /// <summary>
        /// Verifica se o email já está cadastrado no sistema.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> isEmailAlreadyRegister(SignUpDto dto);

        /// <summary>
        /// Verifica se a senha é válida (ou seja, se atende aos critérios de segurança definidos).
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> isPasswordValid(string password);
    }
}