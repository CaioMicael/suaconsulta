using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Repositories;
using suaconsulta_api.Services;
using System.Security.Claims;

namespace suaconsulta_api.Controllers
{
    [Route("api/Doctor")]
    [ApiController]
    public class ControllerDoctor : ControllerApiBase
    {
        public ControllerDoctor(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpGet]
        [Authorize]
        [Route("ListDoctor/")]
        public async Task<IActionResult> GetAsyncListDoctor()
        {
            try
            {
                var doctors = await getRepositoryController<DoctorRepository>().GetDoctorPage();
                return Ok(doctors);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Conflito de concorrência ao criar página.");
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao criar página " + e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetDoctor/")]
        public async Task<IActionResult> GetAsyncDoctorById(
            [FromServices] AppDbContext context,
            [FromQuery] int id)
        {
            var doctor = await context.Doctor.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            return doctor == null ? NotFound() : Ok(doctor);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateDoctor/")]
        public async Task<IActionResult> PostAsyncDoctor([FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await getServiceController<DoctorService>().CreateDoctorDto(dto);
                return Ok("Inserido com sucesso!");
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Conflito de concorrência ao criar o médico.");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Erro de persistência: {ex.Message}");
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao criar Médico " + e.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateDoctor/")]
        public async Task<IActionResult> PutAsyncDoctor([FromBody] UpdateDoctorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado");
            }

            UserExternalInfoDto? UserInfo = await getRepositoryController<userRepository>().getExternalUserInfo(int.Parse(userId));
            if (UserInfo == null || UserInfo.Doctor == null)
            {
                return Unauthorized("Médico não cadastrado");
            }

            dto.Id = UserInfo.Doctor.Id;

            try
            {
                bool res = await getServiceController<DoctorService>().UpdateDoctorDto(dto);
                if (res)
                    return Ok("Atualizado com sucesso!");

                return NotFound("Médico não encontrado");
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Conflito de concorrência ao atualizar o médico.");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Erro de persistência: {ex.Message}");
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
