using suaconsulta_api.Domain.Model;

namespace suaconsulta_api.Application.DTO
{
    /// <summary>
    /// DTO para consultas de um médico e paciente.
    /// </summary>
    public class DoctorPatientConsultations
    {
        public ModelPatient Patient { set; get; }
        public ModelDoctor Doctor { set; get; }
        public ModelConsultation[]? Consultations { set; get; }
    }
}