using suaconsulta_api.Domain.Model;

namespace suaconsulta_api.Application.DTO
{
    /// <summary>
    /// Data Transfer Object para relacionar um usuário com um paciente/médico de acordo com o tipo do usuário.
    /// </summary>
    public class UserExternalInfoDto
    {
        public ModelUsers User { get; set; }
        public ModelPatient? Patient { get; set; }
        public ModelDoctor? Doctor { get; set; }
    }
}
