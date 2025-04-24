using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using suaconsulta_api.Model.Enum;

namespace suaconsulta_api.Model
{
    [Table("consultation")]
    public class ModelConsultation
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        [Column("date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        [Column("status")]
        public EnumStatusConsultation Status { get; set; }

        [Required(ErrorMessage = "Paciente é obrigatório")]
        [Column("patientid")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Médico é obrigatório")]
        [Column("doctorid")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [Column("description")]
        public string Description { get; set; }

        public virtual ModelPatient Patient { get; set; }
        public virtual ModelDoctor Doctor { get; set; }
    }
}
