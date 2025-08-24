using suaconsulta_api.Domain.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.Application.DTO
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        public string mail { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string pass { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        public EnumTypeUsers TypeUser { get; set; }
    }
}
