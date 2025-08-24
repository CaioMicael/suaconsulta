using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Domain.Services;
using suaconsulta_api.DTO;
using suaconsulta_api.Repositories;
using System.Security.Claims;

namespace suaconsulta_api.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class ControllerPatient : ControllerApiBase
    {
        public ControllerPatient(IServiceProvider serviceProvider) : base(serviceProvider) {}

        [HttpGet]
        [Authorize]
        [Route("ListPatient/")]
        public async Task<IActionResult> GetAsyncListPatient([FromServices] AppDbContext context)
        {
            var patients = await context.Patient.OrderBy(L => L.Id).ToListAsync();
            return Ok(patients);
        }

        [HttpGet]
        [Authorize]
        [Route("GetPatientById/")]
        public async Task<IActionResult> GetAsyncPatientById(
            [FromServices] AppDbContext context, 
            [FromQuery] int id)
        {
            var patient = await context.Patient.FirstOrDefaultAsync(i => i.Id == id);
            return patient == null ? NotFound() : Ok(patient);
        }

        [HttpPost]
        [Authorize]
        [Route("CreatePatient/")]
        public async Task<IActionResult> PostAsyncPatient(
            [FromServices] JwtService jwtService,
            [FromServices] AppDbContext context,
            [FromBody] CreatePatientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = new ModelPatient
            {
                Name = dto.Name,
                Email = dto.Email,
                Birthday = dto.Birthday,
                Phone = dto.Phone,
                City = dto.City,
                State = dto.State,
                Country = dto.Country
            };
            try
            {
                await context.Patient.AddAsync(patient);
                await context.SaveChangesAsync();

                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Unauthorized("Usuário não autenticado");

                getServiceController<InterfaceUserRepository>().setExternalId(int.Parse(userId), patient.Id);

                return Ok("Inserido com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao incluir Paciente " + e.Message);
            }
        }

        [HttpPatch]
        [Authorize]
        [Route("PatchPatient/")]
        public async Task<IActionResult> PatchPatient([FromBody] UpdatePatientDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados do paciente são nulos.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound("Usuário não encontrado!");
            }

            UserExternalInfoDto? user = await getRepositoryController<userRepository>().getExternalUserInfo(int.Parse(userId));

            if (user == null || user.Patient == null)
            {
                return NotFound("Paciente não encontrado para este usuário.");
            }

            user.Patient.Name = dto.Name;
            user.Patient.Email = dto.Email;
            user.Patient.Birthday = dto.Birthday;
            user.Patient.Phone = dto.Phone;
            user.Patient.City = dto.City;
            user.Patient.State = dto.State;
            user.Patient.Country = dto.Country;

            try
            {
                if (await getRepositoryController<PatientRepository>().UpdatePatient(user.Patient))
                {
                    return Ok("Dados de paciente alterado com sucesso!");
                }
                return BadRequest("Erro ao atualizar Paciente ");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao atualizar Paciente " + e.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("DeletePatient/")]
        public async Task<IActionResult> DeletePatient(
            [FromServices] AppDbContext context,
            [FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = await context.Patient.FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound("Registro não encontrado.");
            }
            
            try
            {
                context.Patient.Remove(patient);
                await context.SaveChangesAsync();
                return Ok("Excluído com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao excluir Paciente " + e.Message);
            }
        }
    }
}
