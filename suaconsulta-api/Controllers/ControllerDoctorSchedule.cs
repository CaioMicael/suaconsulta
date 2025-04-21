using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Migrations;
using suaconsulta_api.Model;

namespace suaconsulta_api.Controllers
{
    [Route("api/DoctorSchedule")]
    [ApiController]
    public class ControllerDoctorSchedule : ControllerBase
    {
        [HttpGet]
        [Route("ListDoctorSchedule/")]
        public async Task<IActionResult> GetListAsyncDoctorSchedule([FromServices] AppDbContext _context, int DoctorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Response = await _context.DoctorSchedule
                .Include(D => D.Doctor)
                .OrderBy(D => D.Id)
                .Where(D => D.DoctorId == DoctorId)
                .ToListAsync();

            if (Response == null)
            {
                return NotFound();
            }
            return Ok(Response);
        }

        [HttpPost]
        [Route("CreateDoctorSchedule/")]
        public async Task<IActionResult> PostAsyncDoctorSchedule([FromServices] AppDbContext _context, [FromBody] CreateDoctorScheduleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var DoctorSchedule = new ModelDoctorSchedule
            {
                DoctorId = dto.DoctorId,
                EveryWeek = dto.EveryWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            try
            {
                await _context.DoctorSchedule.AddAsync(DoctorSchedule);
                await _context.SaveChangesAsync();
                return Ok("Inserido com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
