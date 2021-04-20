using Social.Model.Modelos;
using System.Linq;

namespace Social.DAL.Contas
{
    public interface ITransferenciaRepository : IRepository<Transferencia>
    {
        IQueryable<Transferencia> GetTransacoesPorContaId(string contaId);
    }
}
