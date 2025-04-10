using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Model;
using suaconsulta_api.Model.Enum;

namespace suaconsulta_api.Controllers
{
    public class ControllerConsultation : ControllerBase
    {
        private static ModelConsultation ModelConsultation { get; set; }

        private static EnumStatusConsultation EnumStatusConsultation { get; set; }

        [HttpGet]
        [Route("api/Consultation/{DoctorId:int}")]
        public async Task<IActionResult> GetMedicalConsultation(
            [FromServices] AppDbContext context,
            [FromRoute] int DoctorId)
        {
            var DoctorConsultations = await context.Consultation.OrderBy(C => C.DoctorId).ToListAsync();
            return Ok(DoctorConsultations);
        }

        [HttpGet]
        [Route("api/Consultation/{PatientId:int}")]
        public async Task<IActionResult> GetPatientConsultation(
            [FromServices] AppDbContext context,
            [FromRoute] int PatientId)
        {
            var PatientConsultations = await context.Consultation.OrderBy(C => C.PatientId).ToListAsync();
            return Ok(PatientConsultations);
        }

        [HttpGet]
        [Route("api/Consultation/ConsultationByDoctorPatient")]
        [Route("{DoctorId:int}/{PatientId:int}")]
        public async Task<IActionResult> GetConsultationByDoctorPatient(
            [FromServices] AppDbContext context,
            [FromRoute] int DoctorId,
            [FromRoute] int PatientId)
        {
            var Consultation = await context.Consultation
                .FirstOrDefaultAsync(C => C.DoctorId == DoctorId && C.PatientId == PatientId);
            return Consultation == null ? NotFound() : Ok(Consultation);
        }

        [HttpPost]
        [Route("api/Consultation")]
        public async Task<IActionResult> PostAsyncConsultation(
            [FromServices] AppDbContext context,
            [FromBody] CreateConsultation dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Consultation = new ModelConsultation
            {
                Date = dto.Date,
                Status = (int)EnumStatusConsultation.Agendada,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId
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
    }
}
