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
        public async Task<string> Alterar(Banco bancoP, int id)
        {
            var banco = await _context
                .Bancos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (banco == null)
            {
                return "Banco não encontrado";
            }
            else
            {
                banco.Nome = bancoP.Nome == null ? banco.Nome : bancoP.Nome;
                banco.Descricao = bancoP.Descricao;
            }

            try
            {
                _context.Bancos.Update(banco);
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                return "Erro ao atualizar -> " + ex;
            }


        } // alterar

        public async Task<Banco> BuscarPorId(int id)
        {
            var banco = await _context
           .Bancos
           .FirstOrDefaultAsync(x => x.Id == id);

            return banco;
        }

        public async Task<IEnumerable<Banco>> BuscarTodos()
        {
            var bancos = await _context
                            .Bancos
                            .AsNoTracking()
                            .ToListAsync();

            return bancos;
        }// buscar todos

        public async Task<string> Excluir(int id)
        {
            var banco = await _context
           .Bancos
           .FirstOrDefaultAsync(x => x.Id == id);

            if (banco == null) return "Banco não encontrado";

            try
            {
                _context.Bancos.Remove(banco);
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                return "Erro ao excluir -> " + ex;
            }
        } //excluir

        public async Task<string> Inserir(Banco banco)
        {
            try
            {
                await _context.Bancos.AddAsync(banco);
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception ex)
            {
                return "Erro ao inserir -> " + ex;
            }
        }//inserir
    }
}
