using suaconsulta_api.Domain.Model;

namespace suaconsulta_api.Application.DTO
{
    public class PatientConsultationsDto
    {
        public ModelPatient Patient { get; set; }

        public List<ModelConsultation>? Consultations { get; set; }
    }
}