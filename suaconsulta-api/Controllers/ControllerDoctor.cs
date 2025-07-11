using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Migrations;
using suaconsulta_api.Model;
using suaconsulta_api.Services;
using System.Security.Claims;

namespace suaconsulta_api.Controllers
{
    [Route("api/Doctor")]
    [ApiController]
    public class ControllerDoctor : ControllerBase
    {
        private static ModelDoctor ModelDoctor = new ModelDoctor();
        private static List<ModelDoctor> ListDoctor = new List<ModelDoctor>();

        [HttpGet]
        [Authorize]
        [Route("ListDoctor/")]
        public async Task<IActionResult> GetAsyncListDoctor([FromServices] AppDbContext context)
        {
            var doctors = await context.Doctor.OrderBy(L => L.Id).ToListAsync();
            return Ok(doctors);
        }

        [HttpGet]
        [Authorize]
        [Route("GetDoctor/")]
        public async Task<IActionResult> GetAsyncDoctorById(
            [FromServices] AppDbContext context,
            [FromQuery] int id)
        {
            var doctor = await context.Doctor.FirstOrDefaultAsync(i => i.Id == id);
            return doctor == null ? NotFound() : Ok(doctor);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateDoctor/")]
        public async Task<IActionResult> PostAsyncDoctor(
            [FromServices] JwtService jwtService,
            [FromServices] AppDbContext context,
            [FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = new ModelDoctor
            {
                Name = dto.Name,
                Specialty = dto.Specialty,
                CRM = dto.CRM,
                Phone = dto.Phone,
                Email = dto.Email,
                City = dto.City,
                State = dto.State,
                Country = dto.Country
            };

            try
            {
                await context.Doctor.AddAsync(doctor);
                await context.SaveChangesAsync();

                var userService = new UserService(context);
                userService.RelateExternalId(User.FindFirstValue(ClaimTypes.NameIdentifier), doctor.Id);

                return Ok("Inserido com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao incluir Médico " + e.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateDoctor/")]
        public async Task<IActionResult> PutAsyncDoctor(
            [FromServices] AppDbContext context,
            [FromBody] UpdateDoctorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = new ModelDoctor
            {
                Name = dto.Name,
                Specialty = dto.Specialty,
                CRM = dto.CRM,
                Phone = dto.Phone,
                Email = dto.Email,
                City = dto.City,
                State = dto.State,
                Country = dto.Country
            };

            try
            {
                context.Doctor.Update(doctor);
                await context.SaveChangesAsync();
                return Ok("Atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao atualizar Médico " + e.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteDoctor/")]
        public async Task<IActionResult> DeleteAsyncDoctor(
            [FromServices] AppDbContext context,
            [FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await context.Doctor.FirstOrDefaultAsync(p => p.Id == id);

            if (doctor == null)
            {
                return NotFound("Registro não encontrado.");
            }

            try
            {
                context.Doctor.Remove(doctor);
                await context.SaveChangesAsync();
                return Ok("Excluído com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao excluir Médico " + e.Message);
            }
        }
    }
}
