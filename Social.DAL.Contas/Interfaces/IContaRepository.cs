using Social.Model.Modelos;

namespace Social.DAL.Contas
{
    public interface IContaRepository : IRepository<Conta>
    {
        Conta FindByConta(string contaId);
    }
}
