using ConFin.Data;
using ConFinServer.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ConFinServer.Controllers
{
	[Route("api/controller")]
	[ApiController]
	public class EstadoController : ControllerBase
	{
		private static List<Estado> lista = new List<Estado>();
        private readonly AppDbContext _context;
		private static Estado estado = new Estado();

        /**
         * <summary>
         * Construtor da classe EstadoController
         * </summary>
         * <param name="context">Contexto do banco de dados</param>
         */
        public EstadoController(AppDbContext context)
        {
            this._context = context;
        }


        [HttpGet]
		public List<Estado> GetEstado()
		{
            var lista = _context.Estado.OrderBy(E => E.Sigla).ToList();
            return lista;
        }

		[HttpGet]
		[Route("Lista")]
		public List<Estado> EstadoLista()
		{
			ArgumentNullException.ThrowIfNull(lista);
			return lista;
		}

		[HttpPost]
		public IActionResult PostEstado(Estado estado)
		{
            try
            {
            _context.Estado.Add(estado);
            _context.SaveChanges();
			return Ok("Estado cadastrado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao incluir o estado: " + e.Message);
            }
		}

		[HttpPut]
		public IActionResult PutEstado([FromBody] Estado estado)
		{
			var EstadoExiste = _context.Estado.Where(l => l.Sigla == estado.Sigla).FirstOrDefault();
			if (EstadoExiste != null)
			{
                try
                {
				EstadoExiste.Nome = estado.Nome;
                _context.Estado.Update(EstadoExiste);
                _context.SaveChanges();
				return Ok("Estado alterado com sucesso!");
                }
                catch (Exception e)
                {
                    return BadRequest("Erro ao alterar o Estado: " + e.Message);
                }
			}
            return NotFound("Estado não encontrado!");
		}

		[HttpDelete]
		public IActionResult DeleteEstado(Estado estado)
		{
            var EstadoExiste = _context.Estado.Where(l => l.Sigla == estado.Sigla).FirstOrDefault();
            if (EstadoExiste != null)
            {
                try {
                _context.Estado.Remove(EstadoExiste);
                _context.SaveChanges();
                return Ok("Estado excluído com sucesso!");
                }
                catch (Exception e) {
                    return BadRequest("Erro ao excluir o Estado: " + e.Message);
                }
            }
            return NotFound("Estado não encontrado!");
        }

        /*
		[HttpDelete("ExcluirByQuery")]
		public string DeleteEstadoByQuery([FromQuery] string sigla)
        {
            var EstadoExiste = lista.Where(l => l.Sigla == sigla).FirstOrDefault();
            if (EstadoExiste != null)
            {
                lista.Remove(EstadoExiste);
                return "Estado excluído com sucesso!";
            }
            return "Estado não encontrado!";
        }

        [HttpDelete("ExcluirByHeader")]
        public string DeleteEstadoByHeader([FromHeader] string sigla)
        {
            var EstadoExiste = lista.Where(l => l.Sigla == sigla).FirstOrDefault();
            if (EstadoExiste != null)
            {
                lista.Remove(EstadoExiste);
                return "Estado excluído com sucesso!";
            }
            return "Estado não encontrado!";
        }

        [HttpDelete("{sigla}")]
        public string DeleteEstadoByRoute([FromRoute] string sigla)
        {
            var EstadoExiste = lista.Where(l => l.Sigla == sigla).FirstOrDefault();
            if (EstadoExiste != null)
            {
                lista.Remove(EstadoExiste);
                return "Estado excluído com sucesso!";
            }
            return "Estado não encontrado!";
        }*/
    }
}
