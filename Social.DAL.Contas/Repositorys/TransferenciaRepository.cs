using Social.Model;
using Social.Model.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace Social.DAL.Contas
{
    public class TransferenciaRepository : BaseRepository<Transferencia>, ITransferenciaRepository
    {
        private readonly SocialContext _context;

        public TransferenciaRepository(SocialContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Transferencia> GetTransacoesPorContaId(string contaId)
        {
            return _context.Set<Transferencia>().Where(x => x.ContaDebito == contaId || x.ContaCredito == contaId);
        }
    }
}
