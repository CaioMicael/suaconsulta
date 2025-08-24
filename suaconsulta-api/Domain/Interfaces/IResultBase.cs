using suaconsulta_api.Domain.Errors;

namespace suaconsulta_api.Domain.Interfaces
{
    /// <summary>
    /// Interface para retorno de resultados da API
    /// </summary>
    public interface IResultBase
    {
        /// <summary>
        /// Indica se o retorno deve ser sucesso
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Indica se o retorno deve ser de objeto criado
        /// </summary>
        bool IsCreated { get; }

        DomainError Error { get; }
        object GetValue();
    }
}