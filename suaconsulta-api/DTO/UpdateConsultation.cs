using System.ComponentModel.DataAnnotations;
using suaconsulta_api.Model.Enum;

namespace suaconsulta_api.DTO
{
    public class UpdateConsultation
    {
        [Required(ErrorMessage = "Id é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        public EnumStatusConsultation Status { get; set; }
    }
}
