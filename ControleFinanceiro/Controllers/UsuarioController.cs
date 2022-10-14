using ControleFinanceiro.Data;
using ControleFinanceiro.Models;
using ControleFinanceiro.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Controllers
{

    [Authorize]
    [Route("v1/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuarios = await _context
                .Usuarios
                .AsNoTracking()
                .ToListAsync();

            return Ok(usuarios);
        } // listar


        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> CriarUsuario([FromBody] Usuario usuarioModel)
        {
            //verifica se os dados sao validos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //verificar se existe algum outro usuario com o mesmo nome de usuário passado
            var usuarioAlterar2 = await _context
                 .Usuarios
                 .AsNoTracking()
                 .FirstOrDefaultAsync(ua2 => ua2.Username == usuarioModel.Username);

            if (usuarioAlterar2 != null)
                return BadRequest(new { message = "Nome de usuário já está sendo usado" });

            //criptografar senha
            usuarioModel.Password = Criptografia.Criptografar(usuarioModel.Password);

            try
            {
                _context.Usuarios.Add(usuarioModel);
                await _context.SaveChangesAsync();

                //esconde a senha quando retorna o model pra tela
                usuarioModel.Password = "";
                return usuarioModel;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Não foi possível criar o usuário -> " + ex });
            }

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Usuario>> Put(int id, [FromBody] Usuario model)
        {
            //verifica se os dados são validos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //verifica se o ID informado é o mesmo do modelo
            if (id != model.Id)
                return NotFound(new { message = "Usuário não encontrado" });

            try
            {
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel atualizar o usuario" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {

            //verifica se usuário existe
            var usuario = await _context
                .Usuarios
                .FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null) return BadRequest(new { message = "Usuário não encontrado" });

            try
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao excluir usuário -> " + ex });
            }
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        //dynamic: as vezes retorna usuario e as vezes nao retorna nada
        public async Task<ActionResult<dynamic>> Login([FromBody] Usuario model)
        {
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .Where(x => x.Username == model.Username)
                .FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            //comparar senha
            if (Criptografia.Comparar(model.Password, usuario.Password))
            {
                var token = TokenService.GenerateToken(usuario);

                //esconde senha
                usuario.Password = "";
                return new
                {
                    usuario = usuario,
                    token = token
                };
            }
            else
            {
                return BadRequest(new { message = "Usuário ou senha inválidos" });
            }

        }
    }
}
