using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Model;
using System.Threading.Tasks;

namespace suaconsulta_api.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class ControllerPatient : ControllerBase
    {
        private readonly AppDbContext _context;
        private static ModelPatient ModelPatient = new ModelPatient();
        private static List<ModelPatient> ListPatient = new List<ModelPatient>();

        [HttpGet]
        public async Task<IActionResult> GetAsyncListPatient([FromServices] AppDbContext context)
        {
            var patients = await context.Patient.OrderBy(L => L.Id).ToListAsync();
            return Ok(patients);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAsyncListPatientById(
            [FromServices] AppDbContext context, 
            [FromRoute] int id)
        {
            var patient = await context.Patient.FirstOrDefaultAsync(i => i.Id == id);
            return patient == null ? NotFound() : Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsyncPatient(
            [FromServices] AppDbContext context,
            [FromBody] CreatePatientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = new ModelPatient
            {
                Name = dto.Name,
                Email = dto.Email,
                Birthday = dto.Birthday,
                Phone = dto.Phone,
                City = dto.City,
                State = dto.State,
                Country = dto.Country
            };
            try
            {
                await context.Patient.AddAsync(patient);
                await context.SaveChangesAsync();
                return Ok("Inserido com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao incluir Paciente " + e.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPatient(
            [FromServices] AppDbContext context,
            [FromRoute] int id, 
            [FromBody] UpdatePatientDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados do paciente são nulos.");
            }

            var patient = await context.Patient.FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound("Registro não encontrado.");
            }

            if (!string.IsNullOrEmpty(dto.Name))
            {
                patient.Name = dto.Name;
            }
            if (!string.IsNullOrEmpty(dto.Email))
            {
                patient.Email = dto.Email;
            }
            if (!string.IsNullOrEmpty(dto.Birthday))
            {
                patient.Birthday = dto.Birthday;
            }
            if (!string.IsNullOrEmpty(dto.Phone))
            {
                patient.Phone = dto.Phone;
            }
            if (!string.IsNullOrEmpty(dto.City))
            {
                patient.City = dto.City;
            }
            if (!string.IsNullOrEmpty(dto.State))
            {
                patient.State = dto.State;
            }
            if (!string.IsNullOrEmpty(dto.Country))
            {
                patient.Country = dto.Country;
            }

            try
            {
                context.Patient.Update(patient);
                await context.SaveChangesAsync();
                return Ok("Atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao atualizar Paciente " + e.Message);
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(
            [FromServices] AppDbContext context,
            int id,
            [FromBody] DeletePatientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = await context.Patient.FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound("Registro não encontrado.");
            }
            
            try
            {
                context.Patient.Remove(patient);
                await context.SaveChangesAsync();
                return Ok("Excluído com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao excluir Paciente " + e.Message);
            }
        }
    }
}
