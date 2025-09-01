namespace suaconsulta_api.Domain.Errors
{
    /// <summary>
    /// Erros de domínio de pacientes
    /// </summary>
    public class UserDomainError : DomainError
    {
        public UserDomainError(string code, string message, int statusCode) : base(code, message, statusCode) { }

        /// <summary>
        /// Retorna erro de not found para pacientes
        /// </summary>
        public static DomainError NotFoundUser =>
            new("NOT_FOUND", "Usuário não encontrado", StatusCodes.Status404NotFound);
    }
}