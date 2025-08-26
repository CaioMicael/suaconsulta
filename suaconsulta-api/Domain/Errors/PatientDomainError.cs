namespace suaconsulta_api.Domain.Errors
{
    /// <summary>
    /// Erros de domínio de pacientes
    /// </summary>
    public class PatientDomainError : DomainError
    {
        public PatientDomainError(string code, string message, int statusCode) : base(code, message, statusCode) { }

        /// <summary>
        /// Retorna erro de not found para pacientes
        /// </summary>
        public static DomainError NotFoundPatient =>
            new("NOT_FOUND", "Paciente não encontrado", StatusCodes.Status404NotFound);
    }
}