using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using suaconsulta_api.Domain.Model.Enum;

namespace suaconsulta_api.Domain.Model
{
    [Table("users")]
    public class ModelUsers
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("externalid")]
        [Required(ErrorMessage = "External ID é obrigatório")]
        public int ExternalId { get; set; }

        [Column("typeuser")]
        [Required(ErrorMessage = "Tipo de usuário é obrigatório")]
        public EnumTypeUsers TypeUser { get; set; }

        [Column("mail")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Mail { get; set; }

        [Column("password")]
        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; }

    }
}
