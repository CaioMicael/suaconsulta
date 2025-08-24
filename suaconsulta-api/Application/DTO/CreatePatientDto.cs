using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.Application.DTO
{
    public class CreatePatientDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido é inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A idade é obrigatória.")]
        [DataType(DataType.Date)]
        public string Birthday { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "O telefone fornecido é inválido.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres.")]
        public string City { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(100, ErrorMessage = "O estado deve ter no máximo 100 caracteres.")]
        public string State { get; set; }

        [Required(ErrorMessage = "O país é obrigatório.")]
        [StringLength(100, ErrorMessage = "O país deve ter no máximo 100 caracteres.")]
        public string Country { get; set; }
    }

}
