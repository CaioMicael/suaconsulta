﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Route("api/Consultation/DoctorConsultations/")]
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
        [Route("api/Consultation/PatientConsultations/")]
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
        [Route("api/Consultation/ConsultationByDoctorPatient/")]
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
        [Route("api/Consultation/ConsultationStatus/")]
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
        [Route("api/Consultation/CreateConsultation")]
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
    }
}
