using ControleFinanceiro.Data;
using ControleFinanceiro.Interfaces;
using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Repositories
{
    public class BancoRepository : IBancoRepository
    {

        //deixar o contexto fora para que nao precise toda horas ficar passando ele entre parametros
        private readonly AppDbContext _context;
        public BancoRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<string> Alterar(Banco banco, int id)
        {
            throw new NotImplementedException();
        }

        public Task<Banco> BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Banco>> BuscarTodos()
        {
            var bancos = await _context
                            .Bancos
                            .AsNoTracking()
                            .ToListAsync();

            return bancos;
        }// buscar todos

        public Task<string> Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Inserir(Banco banco, int id)
        {
            throw new NotImplementedException();
        }
    }
}
