using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.DTO
{
    public class UpdateDoctorScheduleDto
    {
        [Required(ErrorMessage = "Id é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informar a situação do agendamento é obrigatório")]
        public bool Active { get; set; }
    }
}
