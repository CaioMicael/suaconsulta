using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Application.DTO;

namespace suaconsulta_api.Domain.Services
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

        /// <summary>
        /// Realiza o cadastro de um novo usuário.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IActionResult> DoSignUp(SignUpDto dto);

        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IActionResult> DoLogin([FromBody] LoginRequest request);
    }
}