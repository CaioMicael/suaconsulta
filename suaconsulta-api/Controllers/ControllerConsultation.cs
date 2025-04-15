using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Model;
using suaconsulta_api.Model.Enum;
using suaconsulta_api.Validator;

namespace suaconsulta_api.Controllers
{
    [Route("api/Consultation/")]
    public class ControllerConsultation : ControllerBase
    {
        private static CreateConsultationValidator _validator { get; set; }

        public static CreateConsultationValidator InstanceValidator
        {
            get
            {
                if (_validator == null)
                {
                    _validator = new CreateConsultationValidator();
                }
                return _validator;
            }
        }

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

        [HttpPost]
        [Route("CreateConsultation")]
        public async Task<IActionResult> PostAsyncConsultation(
            [FromServices] AppDbContext context,
            [FromBody] CreateConsultation dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (! await this.ExistsPatientAsync(dto.PatientId, context))
            {
                return BadRequest("Paciente não cadastrado!");
            }

            if (!await this.ExistsDoctorAsync(dto.DoctorId, context))
            {
                return BadRequest("Médico não cadastrado!");
            }

            InstanceValidator.ValidateAndThrow(dto);

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

        private async Task<bool> ExistsPatientAsync(int PatientId, AppDbContext context)
        {
            if (await context.Patient.FirstOrDefaultAsync(P => P.Id == PatientId) == null)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ExistsDoctorAsync(int DoctorId, AppDbContext context)
        {
            if (await context.Doctor.FirstOrDefaultAsync(D => D.Id == DoctorId) == null)
            {
                return false;
            }
            return true;
        }
    }
}
