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

        private DomainError(string code, string message, int statusCode)
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

        public static DomainError InvalidId =>
            new("BAD_REQUEST", "Id infomado inválido", StatusCodes.Status400BadRequest);
    }
}