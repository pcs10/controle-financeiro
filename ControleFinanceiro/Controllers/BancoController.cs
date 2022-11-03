using ControleFinanceiro.Interfaces;
using ControleFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Controllers
{
    [ApiController]
    [Route("v1/bancos")]
    public class BancoController : ControllerBase
    {
        public readonly IBancoRepository _bancoService;

        public BancoController(IBancoRepository bancoService)
        {
            _bancoService = bancoService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodosAsync()
        {

            try
            {
                return Ok(await _bancoService.BuscarTodos());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }// listar todos

        [HttpPost]
        public async Task<IActionResult> InserirAsync([FromBody] Banco bancoModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var erro = await _bancoService.Inserir(bancoModel);

                if ((erro == null) || erro == "")
                    return Created($"v1/categorias/{bancoModel.Id}", bancoModel);
                else
                    return BadRequest("ERRO -> " + erro.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest("ERRO -> " + ex);
            }
        }//inserir

    }
}
