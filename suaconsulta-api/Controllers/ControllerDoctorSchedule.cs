using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Model;
using suaconsulta_api.Validator;

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
        public async Task<IActionResult> PostAsyncDoctorSchedule(
            [FromServices] AppDbContext _context, 
            [FromServices] IValidator<CreateDoctorScheduleDto> validator,
            [FromBody] CreateDoctorScheduleDto dto)
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

        [HttpDelete]
        [Route("DeleteDoctorSchedule/")]
        public async Task<IActionResult> DeleteAsyncDoctorSchedule([FromServices] AppDbContext _context, [FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var DoctorSchedule = await _context.DoctorSchedule.FirstOrDefaultAsync(p => p.Id == id);

            if (DoctorSchedule == null)
            {
                return NotFound("Registro não encontrado.");
            }

            try
            {
                _context.DoctorSchedule.Remove(DoctorSchedule);
                await _context.SaveChangesAsync();
                return Ok("Excluído com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao excluir Horário da agenda: " + e.Message);
            }
        }
    }
}
