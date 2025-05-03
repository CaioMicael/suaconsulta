using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Model;
using suaconsulta_api.Model.Enum;

namespace suaconsulta_api.Controllers
{
    [Route("api/Consultation/")]
    public class ControllerConsultation : ControllerBase
    {
        /// <summary>
        /// Endpoint GET assíncrono para busca de todas as consultas.
        /// </summary>
        /// <param name="context">contexto do banco de dados</param>
        /// <param name="DoctorId">Id do médico</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("DoctorConsultations/")]
        public async Task<IActionResult> GetDoctorConsultation(
            [FromServices] AppDbContext context,
            [FromRoute] int DoctorId)
        {
            var DoctorConsultations = await context.Consultation
                .Include(C => C.Doctor)
                .OrderBy(C => C.DoctorId)
                .ToListAsync();
            return Ok(DoctorConsultations);
        }

        /// <summary>
        /// Endpoint GET assíncrono para busca de consulta por paciente.
        /// </summary>
        /// <param name="context">contexto do banco de dados</param>
        /// <param name="PatientId">Id do paciente</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("PatientConsultations/")]
        public async Task<IActionResult> GetPatientConsultation(
            [FromServices] AppDbContext context,
            [FromRoute] int PatientId)
        {
            var PatientConsultations = await context.Consultation
                .Include(C => C.Patient)
                .OrderBy(C => C.PatientId)
                .ToListAsync();
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
                return BadRequest(e.Message);
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
                return BadRequest(e.Message);
            }
        }
    }
}
