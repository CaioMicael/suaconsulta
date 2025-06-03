using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Model;
using suaconsulta_api.Model.Enum;

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

        /// <summary>
        /// Retorna uma lista com os horários disponíveis para o médico selecionado em uma data específica.
        /// </summary>
        /// <param name="DoctorId">Id do médico para o qual se deseja verificar os horários disponíveis.</param>
        /// <param name="year">Ano selecionado para verificar as datas disponíveis.</param>
        /// <param name="month">Mês selecionado para verificar as datas disponíveis.</param>
        /// <returns>Lista de horários disponíveis para o médico na data especificada.</returns>
        [HttpGet]
        [Route("ListAvailableTimes/")]
        public async Task<IActionResult> GetListAsyncAvailableTimes([FromServices] AppDbContext _context, int DoctorId, int year, int month)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Response = await _context.DoctorSchedule
                .Include(D => D.Doctor)
                .Where(D => D.DoctorId == DoctorId &&
                            D.StartTime.Date.Year == year &&
                            D.StartTime.Date.Month == month &&
                            D.Active == true &&
                            !_context.Consultation.Any(C => C.DoctorId == D.DoctorId &&
                                                      C.Date.Year == D.StartTime.Year &&
                                                      C.Date.Month == D.StartTime.Month &&
                                                      C.Date.Day == D.StartTime.Day &&
                                                      C.Date.Hour == D.StartTime.Hour &&
                                                      C.Date.Minute == D.StartTime.Minute &&
                                                      C.Status != EnumStatusConsultation.Concluida)
                            )
                .Select(D => new {
                       D.StartTime ,
                       shortDate = D.StartTime.ToShortDateString() ,
                       D.StartTime.Hour ,
                       D.StartTime.Minute})
                .ToListAsync();

            if (Response == null)
            {
                return NotFound("Nenhum horário disponível para o médico selecionado nesta data.");
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

        [HttpPut]
        [Route("UpdateDoctorSchedule/")]
        public async Task<IActionResult> PutAsyncDoctorSchedule(
            [FromServices] AppDbContext _context, 
            [FromBody] UpdateDoctorScheduleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var DoctorSchedule = await _context.DoctorSchedule.FirstOrDefaultAsync(p => p.Id == dto.Id);

            if (DoctorSchedule == null)
            {
                return NotFound("Registro não encontrado.");
            }

            DoctorSchedule.Active = dto.Active;

            try
            {
                _context.DoctorSchedule.Update(DoctorSchedule);
                await _context.SaveChangesAsync();
                return Ok("Atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao atualizar Horário da agenda: " + e.Message);
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
