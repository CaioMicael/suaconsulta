using ConFin.Data;
using ConFinServer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ConFinServer.Controllers
{
    [Route("api/controllerCidade")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private static CidadeModel ModelCidade = new CidadeModel();
        private static List<CidadeModel> listaCidade = new List<CidadeModel>();
        private readonly AppDbContext _context;

        /**
         * <summary>
         * Construtor da classe CidadeController
         * </summary>
         * <param name="context">Contexto do banco de dados</param>
         */
        public CidadeController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public List<CidadeModel> Listar()
        {
            return _context.Cidade.OrderBy(C => C.Codigo).ToList();
        }

        [HttpPost]
        public IActionResult incluirCidade(CidadeModel ModelCidade )
        {
            var Sigla = ModelCidade.Estado;
            var EstadoExiste = _context.Estado.Where(lc => lc.Sigla == Sigla).FirstOrDefault();
            if (EstadoExiste == null)
            {
                return NotFound("O estado informado não existe.");
            }
            try
            {
                _context.Cidade.Add(ModelCidade);
                _context.SaveChanges();
                return Ok("Cidade cadastrada com sucesso.");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao incluir a cidade: " + e.Message);
            }
        }

        [HttpPut]
        public IActionResult alterarCidade(CidadeModel ModelCidade )
        {
            var Sigla = ModelCidade.Estado;
            var CidadeExiste = _context.Cidade.Where(lc => lc.Codigo == ModelCidade.Codigo).FirstOrDefault();
            var EstadoExiste = _context.Estado.Where(lc => lc.Sigla == Sigla).FirstOrDefault();
            if (CidadeExiste != null)
            {
                if (EstadoExiste == null)
                {
                    return NotFound("O estado informado não existe.");
                }
                CidadeExiste.Codigo = ModelCidade.Codigo;
                CidadeExiste.Nome   = ModelCidade.Nome;
                CidadeExiste.Estado = ModelCidade.Estado;
                try
                {
                    _context.Cidade.Update(CidadeExiste);
                    _context.SaveChanges();
                    return Ok("A cidade de código " + CidadeExiste.Codigo + " Foi alterada com sucesso.");
                }
                catch (Exception e)
                {
                    return BadRequest("Erro ao alterar a cidade: " + e.Message);
                }
            }
            return NotFound("Não encontramos a cidade para alterar.");
        }

        [HttpDelete]
        public IActionResult excluirCidade(CidadeModel CidadeModel)
        {
            var CidadeExiste = _context.Cidade.Where(lc => lc.Codigo == CidadeModel.Codigo).FirstOrDefault();
            if (CidadeExiste != null)
            {
                try
                {
                    _context.Cidade.Remove(CidadeExiste);
                    _context.SaveChanges();
                    return Ok("A cidade de código " + CidadeExiste.Codigo + " foi excluída com sucesso.");
                }
                catch (Exception e)
                {
                    return BadRequest("Erro ao excluir a cidade: " + e.Message);
                }
            }
            return NotFound("Não encontramos a cidade para excluir.");
        }
    }
}
