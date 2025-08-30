namespace suaconsulta_api.Domain.Errors
{
    /// <summary>
    /// Erros de domínio de consultas
    /// </summary>
    public class ConsultationDomainError : DomainError
    {
        public ConsultationDomainError(string code, string message, int statusCode) : base(code, message, statusCode) { }

        /// <summary>
        /// Lança um not found para consultas
        /// </summary>
        public static DomainError NotFoundConsultation =>
            new("NOT_FOUND", "Consulta não encontrada", StatusCodes.Status404NotFound);

        /// <summary>
        /// Lança um not found para status da consulta
        /// </summary>
        public static DomainError NotFoundStatusConsultation =>
            new("NOT_FOUND", "Status da consulta não encontrado", StatusCodes.Status404NotFound);

        public static DomainError BadRequestConsultationDateLessNow =>
            new("BAD_REQUEST", "A data da consulta é menor que a data atual", StatusCodes.Status404NotFound);

        public static DomainError BadRequestDateForConsultationNotAvailable =>
            new("BAD_REQUEST", "A data informada para consulta não está disponível na agenda do médico", StatusCodes.Status400BadRequest);
    }
}