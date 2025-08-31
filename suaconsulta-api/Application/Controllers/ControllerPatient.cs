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
    [Route("api/Patient")]
    [ApiController]
    public class ControllerPatient : ControllerBase
    {
        private readonly PatientService _patientService;
        private readonly PatientRepository _patientRepository;
        private readonly InterfaceUserRepository _userRepository;

        public ControllerPatient(
            PatientService patientService, 
            PatientRepository patientRepository,
            InterfaceUserRepository userRepository
            )
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

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
        public async Task<Result<ModelPatient>> GetAsyncPatientById([FromQuery] int id)
        {
            ModelPatient? patient = await _patientRepository.GetPatientById(id);
            if (patient == null)
                return Result<ModelPatient>.Failure(PatientDomainError.NotFoundPatient);

            return Result<ModelPatient>.Success(patient);
        }

        [HttpPost]
        [Authorize]
        [Route("CreatePatient/")]
        public async Task<Result<string>> PostAsyncPatient([FromBody] CreatePatientDto dto, [FromServices] AppDbContext context)
        {
            if (!ModelState.IsValid)
            {
                return Result<string>.Failure(DomainError.GenericNotFound);
            }

            ModelPatient patient = new ModelPatient
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
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return Result<string>.Failure(DomainError.Unauthorized);

                await context.Patient.AddAsync(patient);
                await context.SaveChangesAsync();

                return Result<string>.Success("Inserido com sucesso!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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

            UserExternalInfoDto? user = await _userRepository.getExternalUserInfo(int.Parse(userId));

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
                if (await _patientRepository.UpdatePatient(user.Patient))
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
