using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.Application.DTO
{
    public class DeleteDoctorScheduleDto
    {
        [Required(ErrorMessage = "Id é obrigatório")]
        public int Id { get; set; }
    }
}
