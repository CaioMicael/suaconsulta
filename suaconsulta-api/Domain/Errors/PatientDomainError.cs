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

        /// <summary>
        /// Retorna erro de not found quando o usuário vinculado ao paciente não existe
        /// </summary>
        public static DomainError PatientNotFoundForUser =>
            new("NOT_FOUND", "Paciente não encontrado para este usuário", StatusCodes.Status404NotFound);

        /// <summary>
        /// Retorna erro de bad request quando os dados do paciente enviados são nulos
        /// </summary>
        public static DomainError PatientNullBadRequest =>
            new("BAD_REQUEST", "Dados do paciente são nulos", StatusCodes.Status400BadRequest);

    }
}