using Microsoft.AspNetCore.Mvc;
using suaconsulta_api.Data;
using suaconsulta_api.Model;

namespace suaconsulta_api.Controllers
{
    [Route("api/Doctor")]
    [ApiController]
    public class ControllerDoctor : ControllerBase
    {
        private static ModelDoctor ModelDoctor = new ModelDoctor();
        private static List<ModelDoctor> ListDoctor = new List<ModelDoctor>();
        private readonly AppDbContext _context;

        /**
         * ControllerDoctor
         * Método construtor da classe
         * @param context
         */
        public ControllerDoctor(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<ModelDoctor> GetListDoctor()
        {
            return _context.Doctor.OrderBy(L => L.Id).ToList();
        }

        [HttpPost]
        public IActionResult PostDoctor([FromBody] ModelDoctor doctor)
        {
            try
            {
            _context.Doctor.Add(doctor);
            _context.SaveChanges();
            return Ok("Inserido com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao incluir Médico " + e.Message);
            }
        }

        [HttpPut]
        public IActionResult PutDoctor([FromBody] ModelDoctor doctor)
        {
            try
            {
                _context.Doctor.Update(doctor);
                _context.SaveChanges();
                return Ok("Atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao atualizar Médico " + e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            try
            {
                ModelDoctor doctor = _context.Doctor.Find(id);
                _context.Doctor.Remove(doctor);
                _context.SaveChanges();
                return Ok("Excluído com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao excluir Médico " + e.Message);
            }
        }
    }
}
