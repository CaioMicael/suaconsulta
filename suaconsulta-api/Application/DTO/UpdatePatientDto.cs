using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.Application.DTO
{
    public class UpdatePatientDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido é inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        public string Birthday { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "O telefone fornecido é inválido.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(100, ErrorMessage = "O estado deve ter no máximo 100 caracteres.")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "O país é obrigatório.")]
        [StringLength(100, ErrorMessage = "O país deve ter no máximo 100 caracteres.")]
        public string Country { get; set; } = string.Empty;
    }
}
