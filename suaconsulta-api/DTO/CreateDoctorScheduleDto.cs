using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.DTO
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
