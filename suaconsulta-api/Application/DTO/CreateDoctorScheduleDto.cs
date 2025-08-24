using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.Application.DTO
{
    public class CreateDoctorScheduleDto
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public bool EveryWeek { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
