using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.DTO
{
    public class UpdatePatientDto
    {
        [Required(ErrorMessage = "O id é obrigatório.")]
        public int Id { get; set; }

        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "O email fornecido é inválido.")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public string Birthday { get; set; }

        [Phone(ErrorMessage = "O telefone fornecido é inválido.")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres.")]
        public string City { get; set; }

        [StringLength(100, ErrorMessage = "O estado deve ter no máximo 100 caracteres.")]
        public string State { get; set; }

        [StringLength(100, ErrorMessage = "O país deve ter no máximo 100 caracteres.")]
        public string Country { get; set; }
    }
}
