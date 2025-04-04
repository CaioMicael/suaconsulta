using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace suaconsulta_api.Model
{
    public class ModelDoctor
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Especialidade é obrigatória")]
        public string Specialty { get; set; }

        [Required(ErrorMessage = "CRM é obrigatório")]
        public string CRM { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatória")]
        public string City { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        public string State { get; set; }

        [Required(ErrorMessage = "País é obrigatório")]
        public string Country { get; set; }
    }
}
