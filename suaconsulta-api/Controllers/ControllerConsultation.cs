using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Model;

namespace suaconsulta_api.Controllers
{
    public class ControllerConsultation : ControllerBase
    {
        private static ModelConsultation ModelConsultation { get; set; }

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
    }
}
