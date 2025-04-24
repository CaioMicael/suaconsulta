using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace suaconsulta_api.Model
{
    [Table("doctor")]
    public class ModelDoctor
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [Column("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Especialidade é obrigatória")]
        [Column("specialty")]
        public string Specialty { get; set; }

        [Required(ErrorMessage = "CRM é obrigatório")]
        [Column("crm")]
        public string CRM { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [Column("phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Column("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatória")]
        [Column("city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório")]
        [Column("state")]
        public string State { get; set; }

        [Required(ErrorMessage = "País é obrigatório")]
        [Column("country")]
        public string Country { get; set; }
    }
}
