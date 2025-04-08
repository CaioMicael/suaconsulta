using suaconsulta_api.Model;
using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.DTO
{
    public class UpdateConsultation
    {
        [Required(ErrorMessage = "Data é obrigatória")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        public string Status { get; set; }
    }
}
