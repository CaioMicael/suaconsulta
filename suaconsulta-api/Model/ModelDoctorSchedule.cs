using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace suaconsulta_api.Model
{
    public class ModelDoctorSchedule
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool EveryWeek { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        
        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool Active { get; set; }

        public virtual ModelDoctor Doctor { get; set; }
    }
}
