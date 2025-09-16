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

        [HttpPatch]
        [Authorize]
        [Route("PatchPatient/")]
        public async Task<Result<string>> PatchPatient([FromBody] UpdatePatientDto dto)
        {
            if (dto == null)
                return Result<string>.Failure(PatientDomainError.PatientNullBadRequest);

            if (!ModelState.IsValid)
                return Result<string>.Failure(DomainError.GenericBadRequest);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Result<string>.Failure(UserDomainError.NotFoundUser);

            return await _patientService.AlterPatient(dto, int.Parse(userId));
        }
    }
}
