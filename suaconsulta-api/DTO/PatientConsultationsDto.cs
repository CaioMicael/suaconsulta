using suaconsulta_api.Domain.Model;

namespace suaconsulta_api.DTO
{
    public class PatientConsultationsDto
    {
        public ModelPatient Patient { get; set; }

        public List<ModelConsultation>? Consultations { get; set; }
    }
}   