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

        [HttpGet]
        [Route(template: ("{id}"))]
        public async Task<IActionResult> ListarPorIdAsync([FromRoute] int id)
        {
            try
            {
                var banco = await _bancoService.BuscarPorId(id);

                if (banco == null)
                {
                    return BadRequest("ERRO -> banco não encontrado");
                }
                else
                {
                    return Ok(banco);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }//listar um

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

        [HttpPut(template: ("{id}"))]
        public async Task<IActionResult> AlterarAsync([FromBody] Banco bancoModel, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var banco = await _bancoService.Alterar(bancoModel, id);

                if ((banco == null) || banco == "")
                    return Ok(banco);
                else
                    return BadRequest("ERRO -> " + banco);
            }
            catch (Exception ex)
            {
                return BadRequest("ERRO -> " + ex);
            }

        } //alterar

        [HttpDelete(template: ("{id}"))]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var erro = await _bancoService.Excluir(id);

                if ((erro == null) || erro == "")
                    return Ok("Banco excluído com sucesso");
                else
                    return BadRequest("ERRO -> " + erro);
            }
            catch (Exception ex)
            {
                return BadRequest("ERRO -> " + ex);
            }
        }//excluir

    }
}
