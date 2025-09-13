using suaconsulta_api.Domain.Model.Enum;

namespace suaconsulta_api.Application.DTO
{
    /// <summary>
    /// Retorna o token e o tipo do usuário
    /// </summary>
    public class TokenInformationDto
    {
        public string token { get; set; }

        public EnumTypeUsers role { get; set; }
    }
}
