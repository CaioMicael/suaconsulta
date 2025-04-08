using suaconsulta_api.Model;
using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.DTO
{
    public class CreateConsultation
    {
        [Required(ErrorMessage = "Data é obrigatória")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Paciente é obrigatório")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Médico é obrigatório")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string Description { get; set; }
    }
}
