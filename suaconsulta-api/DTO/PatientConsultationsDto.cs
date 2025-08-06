using suaconsulta_api.Model;

namespace suaconsulta_api.DTO
{
    public class PatientConsultationsDto
    {
        public ModelPatient Patient { get; set; }

        public List<ModelConsultation>? Consultations { get; set; }
    }
}   