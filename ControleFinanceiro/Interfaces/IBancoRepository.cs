using ControleFinanceiro.Models;

namespace ControleFinanceiro.Interfaces
{
    public interface IBancoRepository
    {
        Task<IEnumerable<Banco>> BuscarTodos();
        Task<Banco> BuscarPorId(int id);
        Task<string> Inserir(Banco banco, int id);
        Task<string> Alterar(Banco banco, int id);
        Task<string> Excluir(int id);
    }
}
