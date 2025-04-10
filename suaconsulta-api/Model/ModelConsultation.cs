using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace suaconsulta_api.Model
{
    public class ModelConsultation
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        public int Status { get; set; }

        [Required(ErrorMessage = "Paciente é obrigatório")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Médico é obrigatório")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string Description { get; set; }

        public virtual ModelPatient Patient { get; set; }
        public virtual ModelDoctor Doctor { get; set; }
    }
}
