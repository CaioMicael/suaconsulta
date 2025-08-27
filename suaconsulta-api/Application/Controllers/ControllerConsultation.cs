using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Domain.Model.Enum;
using suaconsulta_api.Domain.Model;
using suaconsulta_api.Domain.Services;
using suaconsulta_api.Application.DTO;
using suaconsulta_api.Infrastructure.Repositories;
using suaconsulta_api.Infrastructure.Data;
using suaconsulta_api.Core.Common;
using suaconsulta_api.Domain.Errors;

namespace suaconsulta_api.Application.Controllers
{
    [Route("api/Consultation/")]
    public class ControllerConsultation : ControllerApiBase
    {

        public ControllerConsultation(IServiceProvider serviceProvider) : base(serviceProvider) { }

        /// <summary>
        /// Endpoint GET assíncrono para busca de todas as consultas.
        /// </summary>
        /// <returns>Result<DoctorConsultationsDto></returns>
        [HttpGet]
        [Authorize]
        [Route("DoctorConsultations/")]
        public async Task<Result<DoctorConsultationsDto>> GetDoctorConsultation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Result<DoctorConsultationsDto>.Failure(DomainError.Unauthorized);
            }

            UserExternalInfoDto? UserInfo = await getRepositoryController<userRepository>().getExternalUserInfo(int.Parse(userId));
            if (UserInfo == null || UserInfo.Doctor == null)
            {
                return Result<DoctorConsultationsDto>.Failure(DoctorDomainError.NotFoundDoctor);
            }
            
            return await getServiceController<ConsultationService>().GetDoctorConsultation(UserInfo.Doctor);
        }

        /// <summary>
        /// Endpoint GET assíncrono para busca de consulta por paciente.
        /// </summary>
        /// <returns>Result<PatientConsultationsDto></returns>
        [HttpGet]
        [Authorize]
        [Route("PatientConsultations/")]
        public async Task<Result<PatientConsultationsDto>> GetPatientConsultation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Result<PatientConsultationsDto>.Failure(DomainError.Unauthorized);
            }

            UserExternalInfoDto? UserInfo = await getRepositoryController<userRepository>().getExternalUserInfo(int.Parse(userId));
            if (UserInfo == null || UserInfo.Patient == null)
            {
                return Result<PatientConsultationsDto>.Failure(PatientDomainError.NotFoundPatient);
            }

            return await getServiceController<ConsultationService>().GetPatientConsultations(UserInfo.Patient.Id);
        }

        /// <summary>
        /// Endpoint GET assíncrono para busca de consulta por médico e paciente.
        /// </summary>
        /// <param name="DoctorId">Id do médico</param>
        /// <param name="PatientId">Id do paciente</param>
        /// <returns>Result<DoctorPatientConsultations></returns>
        [HttpGet]
        [Authorize]
        [Route("ConsultationByDoctorPatient/")]
        public async Task<Result<DoctorPatientConsultations>> GetConsultationByDoctorPatient(int DoctorId, int PatientId)
        {
            return await getServiceController<ConsultationService>().GetConsultationByDoctorPatient(DoctorId, PatientId);
        }

        /// <summary>
        /// Endpoint GET assíncrono que retonra o status da consulta por id.
        /// </summary>
        /// <param name="id">ID da consulta</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Authorize]
        [Route("ConsultationStatus/")]
        public async Task<Result<string>> GetConsultationStatusById(int id)
        {
            return await getServiceController<ConsultationService>().GetConsultationStatusById(id);
        }

        /// <summary>
        /// Endpoint POST assíncrono para criação de consultas.
        /// </summary>
        /// <param name="validator">validações da criação de consulta</param>
        /// <param name="dto">DTO do create consultation</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Authorize]
        [Route("CreateConsultation")]
        public async Task<IActionResult> PostAsyncConsultation(
            [FromServices] AppDbContext context,
            [FromServices] IValidator<CreateConsultation> validator,
            [FromBody] CreateConsultation dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));
            }

            var Consultation = new ModelConsultation
            {
                Date = dto.Date,
                Status = EnumStatusConsultation.Agendada,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Description = dto.Description
            };

            try
            {
                await context.Consultation.AddAsync(Consultation);
                await context.SaveChangesAsync();
                return Ok("Inserido com sucesso!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Endpoint PATCH assíncrono para alteração de consultas.
        /// </summary>
        /// <param name="validator">validações da alteração de consulta</param>
        /// <param name="dto">DTO do update consultation</param>
        /// <returns>IActionResult</returns>
        [HttpPatch]
        [Authorize]
        [Route("UpdateConsultation")]
        public async Task<IActionResult> UpdateConsultation(
            [FromServices] AppDbContext context,
            [FromServices] IValidator<UpdateConsultation> validator,
            [FromBody] UpdateConsultation dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));
            }

            var Consultation = await context.Consultation.FirstOrDefaultAsync(C => C.Id == dto.Id);

            if (Consultation == null)
            {
                return NotFound();
            }

            try
            {
                Consultation.Status = dto.Status;
                Consultation.Date = dto.Date;
                context.Consultation.Update(Consultation);
                await context.SaveChangesAsync();
                return Ok("Atualizado com sucesso!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
