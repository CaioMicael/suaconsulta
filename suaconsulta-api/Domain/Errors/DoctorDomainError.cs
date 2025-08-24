namespace suaconsulta_api.Domain.Errors
{
    /// <summary>
    /// Classe de erros de domínio para doctor
    /// </summary>
    public class DoctorDomainError : DomainError
    {
        protected DoctorDomainError(string code, string message, int statusCode) : base(code, message, statusCode) { }

        /// <summary>
        /// Retorna not found para médico
        /// </summary>
        public static DomainError NotFoundDoctor =>
            new("NOT_FOUND", "Médico não encontrado", StatusCodes.Status404NotFound);
    }
}