using ControleFinanceiro.Interfaces;
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

    }
}
