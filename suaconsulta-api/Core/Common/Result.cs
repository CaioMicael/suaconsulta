using suaconsulta_api.Domain.Errors;
using suaconsulta_api.Domain.Interfaces;

namespace suaconsulta_api.Core.Common
{
    /// <summary>
    /// Clase comum para retorno de resultados
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : IResultBase
    {
        public T? Value { get; }
        public bool IsSuccess { get; }
        public DomainError? Error { get; }

        private Result(T? value, bool issucess, DomainError? error)
        {
            Value = value;
            IsSuccess = issucess;
            Error = error;
        }

        /// <summary>
        /// Retorna sucesso na execução
        /// </summary>
        /// <param name="value">Objeto de retorno de sucesso</param>
        /// <returns>Result sucess</returns>
        public static Result<T> Success(T value) =>
            new(value, true, null);

        /// <summary>
        /// Retorna um erro ocorrido.
        /// </summary>
        /// <param name="error">Deve ser passado um domain error conhecido</param>
        /// <returns>Result failure</returns>
        public static Result<T> Failure(DomainError error) =>
            new(default, false, error);

        object IResultBase.GetValue() => Value;
    }
}