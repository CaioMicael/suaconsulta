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
            var patient = _context.Patient.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound("Registro não encontrado.");
            }

            foreach (var prop in typeof(UpdatePatientDto).GetProperties())
            {
                var DtoValue = prop.GetValue(dto);
                if (DtoValue != null)
                {
                    var PatientProperty = typeof(ModelPatient).GetProperty(prop.Name);
                    if (PatientProperty != null && PatientProperty.CanWrite)
                    {
                        PatientProperty.SetValue(patient, DtoValue);
                    }
                }
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
    }
}
