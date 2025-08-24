using System.ComponentModel.DataAnnotations;
using suaconsulta_api.Domain.Model;

namespace suaconsulta_api.Application.DTO
{
    public class DoctorConsultationsDto
    {
        [Required(ErrorMessage = "Médico obrigatório")]
        public ModelDoctor Doctor { get; set; }

        public List<ModelConsultation> Consultation { get; set; }
    }
}
