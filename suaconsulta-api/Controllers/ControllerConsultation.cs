using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Model;
using suaconsulta_api.Model.Enum;
using suaconsulta_api.Repositories;
using suaconsulta_api.Services;

namespace suaconsulta_api.Controllers
{
    [Route("api/Consultation/")]
    public class ControllerConsultation : ControllerApiBase
    {

        public ControllerConsultation(IServiceProvider serviceProvider) : base(serviceProvider) {}

        /// <summary>
        /// Endpoint GET assíncrono para busca de todas as consultas.
        /// </summary>
        /// <param name="context">contexto do banco de dados</param>
        /// <param name="DoctorId">Id do médico</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Authorize]
        [Route("DoctorConsultations/")]
        public async Task<IActionResult> GetDoctorConsultation()
        {
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

            DoctorConsultationsDto DoctorConsultations = await getServiceController<ConsultationService>().GetDoctorConsultation(UserInfo.Doctor);
            return Ok(DoctorConsultations);
        }

        /// <summary>
        /// Endpoint GET assíncrono para busca de consulta por paciente.
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Authorize]
        [Route("PatientConsultations/")]
        public async Task<IActionResult> GetPatientConsultation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("Usuário não autenticado");
            }

            UserExternalInfoDto? UserInfo = await getRepositoryController<userRepository>().getExternalUserInfo(int.Parse(userId));
            if (UserInfo == null || UserInfo.Patient == null) {
                return Unauthorized("Paciente não cadastrado");
            }

            var PatientConsultations = await getRepositoryController<ConsultationRepository>().GetPatientConsultations(UserInfo.Patient.Id);
            return Ok(PatientConsultations);
        }

        /// <summary>
        /// Endpoint GET assíncrono para busca de consulta por médico e paciente.
        /// </summary>
        /// <param name="context">contexto do banco de dados</param>
        /// <param name="DoctorId">Id do médico</param>
        /// <param name="PatientId">Id do paciente</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Authorize]
        [Route("ConsultationByDoctorPatient/")]
        public async Task<IActionResult> GetConsultationByDoctorPatient(
            [FromServices] AppDbContext context,
            int DoctorId,
            int PatientId)
        {
            var Consultation = await context.Consultation
                .Include(C => C.Doctor)
                .Include(C => C.Patient)
                .FirstOrDefaultAsync(C => C.DoctorId == DoctorId && C.PatientId == PatientId);
            return Consultation == null ? NotFound() : Ok(Consultation);
        }

        /// <summary>
        /// Endpoint GET assíncrono que retonra o status da consulta por id.
        /// </summary>
        /// <param name="context">contexto do banco de dados</param>
        /// <param name="id">ID da consulta</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Authorize]
        [Route("ConsultationStatus/")]
        public async Task<IActionResult> GetConsultationStatusById(
            [FromServices] AppDbContext context,
            int id)
        {
            var Consultation = await context.Consultation.FirstOrDefaultAsync(C => C.Id == id);

            if (Consultation == null)
            {
                return NotFound();
            }

            var Response = EnumStatusConsultation.GetName(typeof (EnumStatusConsultation), Consultation.Status);
            return Ok(Response);
        }

        /// <summary>
        /// Endpoint POST assíncrono para criação de consultas.
        /// </summary>
        /// <param name="context">contexto do banco de dados</param>
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
        /// <param name="context">contexto do banco de dados</param>
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
                Consultation.Status = (EnumStatusConsultation)dto.Status;
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
