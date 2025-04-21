using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.DTO
{
    public class CreateDoctorSchedule
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        private bool EveryWeek { get; set; }

        [Required]
        private DateTime StartTime { get; set; }

        [Required]
        private DateTime EndTime { get; set; }
    }
}
