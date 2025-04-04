using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Data;
using suaconsulta_api.DTO;
using suaconsulta_api.Model;

namespace suaconsulta_api.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class ControllerPatient : ControllerBase
    {
        private readonly AppDbContext _context;
        private static ModelPatient ModelPatient = new ModelPatient();
        private static List<ModelPatient> ListPatient = new List<ModelPatient>();

        /**
         * ControllerPatient
         * Método construtor da classe
         * @param context
         */
        public ControllerPatient(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<ModelPatient> GetListPatient()
        {
            return _context.Patient.OrderBy(L => L.Id).ToList();
        }

        [HttpPost]
        public IActionResult PostPatient([FromBody] CreatePatientDto dto)
        {
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
                _context.Patient.Add(patient);
                _context.SaveChanges();
                return Ok("Inserido com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao incluir Paciente " + e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutPatient(int id, [FromBody] UpdatePatientDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados do paciente são nulos.");
            }

            var patient = _context.Patient.FirstOrDefault(p => p.Id == id);
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
                _context.SaveChanges();
                return Ok("Atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao atualizar Paciente " + e.Message);
            }
        }



        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            var patient = _context.Patient.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound("Registro não encontrado.");
            }

            try
            {
                _context.Patient.Remove(patient);
                _context.SaveChanges();
                return Ok("Excluído com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao excluir Paciente " + e.Message);
            }
        }
    }
}
