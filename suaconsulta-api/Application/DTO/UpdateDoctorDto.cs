using System.ComponentModel.DataAnnotations;

namespace suaconsulta_api.Application.DTO
{
    public class UpdateDoctorDto
    {
        [Required(ErrorMessage = "Id é obrigatório")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Specialty { get; set; }

        public string CRM { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}
