namespace suaconsulta_api.Domain.Errors
{
    /// <summary>
    /// Clase para lançar erros de domínio da aplicação, utilizando Domain Error Pattern
    /// </summary>
    public class DomainError
    {
        public string Code { get; }
        public string Message { get; }
        public int StatusCode { get; }

        public DomainError(string code, string message, int statusCode)
        {
            Code = code;
            Message = message;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Dispara erro genérico de Not Found
        /// </summary>
        public static DomainError GenericNotFound =>
            new("NOT_FOUND", "Recurso não encontrado", StatusCodes.Status404NotFound);

        /// <summary>
        /// Retorna bad request padrão
        /// </summary>
        public static DomainError GenericBadRequest =>
            new("BAD_REQUEST", "Requisição está incorreta", StatusCodes.Status400BadRequest);

        /// <summary>
        /// Dispara erro genérico de Unauthorized
        /// </summary>
        public static DomainError Unauthorized =>
            new("UNAUTHORIZED", "Usuário não autenticado", StatusCodes.Status401Unauthorized);

        /// <summary>
        /// Dispara erro genérico de Forbidden
        /// </summary>
        public static DomainError InvalidId =>
            new("FORBIDDEN", "Id infomado inválido", StatusCodes.Status403Forbidden);
    }
}