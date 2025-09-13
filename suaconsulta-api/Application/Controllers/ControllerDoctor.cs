using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Application.DTO;
using suaconsulta_api.Core.Common;
using suaconsulta_api.Domain.Errors;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Domain.Services;
using suaconsulta_api.Infrastructure.Data;
using suaconsulta_api.Infrastructure.Repositories;
using System.Security.Claims;

namespace suaconsulta_api.Application.Controllers
{
    [Route("api/Doctor")]
    [ApiController]
    public class ControllerDoctor : ControllerApiBase
    {
        public ControllerDoctor(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpGet]
        [Authorize]
        [Route("DoctorPage/")]
        public async Task<Result<PagedResultDto<ModelDoctor>>> GetAsyncDoctorPage()
        {
            try
            {
                return await getRepositoryController<DoctorRepository>().GetDoctorPage();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao criar página " + e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetDoctor/")]
        public async Task<Result<ModelDoctor>> GetAsyncDoctorById([FromQuery] int id)
        {
            try
            {
                return await getServiceController<DoctorService>().GetDoctorById(id);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar Médico " + e.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateDoctor/")]
        public async Task<Result<bool>> PutAsyncDoctor([FromBody] UpdateDoctorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Result<bool>.Failure(DomainError.GenericBadRequest);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Result<bool>.Failure(DomainError.Unauthorized);
            }

            UserExternalInfoDto? UserInfo = await getRepositoryController<userRepository>().getExternalUserInfo(int.Parse(userId));
            if (UserInfo == null || UserInfo.Doctor == null)
            {
                return Result<bool>.Failure(DoctorDomainError.NotFoundDoctor);
            }

            dto.Id = UserInfo.Doctor.Id;

            try
            {
                return await getServiceController<DoctorService>().UpdateDoctorDto(dto);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar Médico " + e.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteDoctor/")]
        public async Task<IActionResult> DeleteAsyncDoctor(
            [FromServices] AppDbContext context,
            [FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await context.Doctor.FirstOrDefaultAsync(p => p.Id == id);

            if (doctor == null)
            {
                return NotFound("Registro não encontrado.");
            }

            try
            {
                context.Doctor.Remove(doctor);
                await context.SaveChangesAsync();
                return Ok("Excluído com sucesso!");
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao excluir Médico " + e.Message);
            }
        }
    }
}
