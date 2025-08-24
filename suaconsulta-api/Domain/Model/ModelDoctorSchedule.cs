using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace suaconsulta_api.Domain.Model
{
    [Table("doctorschedule")]
    public class ModelDoctorSchedule
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Doctor")]
        [Column("doctorid")]
        public int DoctorId { get; set; }

        [Required]
        [DefaultValue(false)]
        [Column("everyweek")]
        public bool EveryWeek { get; set; }

        [Required]
        [Column("starttime")]
        public DateTime StartTime { get; set; }

        [Required]
        [Column("endtime")]
        public DateTime EndTime { get; set; }

        [Required]
        [DefaultValue(true)]
        [Column("active")]
        public bool Active { get; set; }

        public virtual ModelDoctor Doctor { get; set; }
    }
}
