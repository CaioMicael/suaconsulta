namespace suaconsulta_api.Domain.Errors
{
    /// <summary>
    /// Erros de domínio de autenticação
    /// </summary>
    public class AuthDomainError : DomainError
    {
        public AuthDomainError(string code, string message, int statusCode) : base(code, message, statusCode) { }

        /// <summary>
        /// Retorna erro de conflict de email já cadastrado
        /// </summary>
        public static DomainError EmailAlreadyRegister =>
            new("CONFLICT", "Email já cadastrado", StatusCodes.Status409Conflict);

        /// <summary>
        /// Retorna erro de bad request de senha inválida
        /// </summary>
        public static DomainError InvalidPassword =>
            new("BAD_REQUEST", "Senha deve ter pelo menos 6 caracteres", StatusCodes.Status400BadRequest);

        /// <summary>
        /// Retorna erro de tipo de usuário inválido
        /// </summary>
        public static DomainError InvalidTypeUser =>
            new("BAD_REQUEST", "Tipo de usuário informado deve estar entre 1 e 2", StatusCodes.Status400BadRequest);
    }
}