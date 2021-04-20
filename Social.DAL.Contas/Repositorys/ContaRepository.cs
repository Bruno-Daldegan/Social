using Social.Model;
using Social.Model.Modelos;
using System.Linq;

namespace Social.DAL.Contas
{
    public class ContaRepository : BaseRepository<Conta>, IContaRepository
    {
        private readonly SocialContext _context;

        public ContaRepository(SocialContext context) : base(context)
        {
            _context = context;
        }

        public Conta FindByConta(string contaId)
        {
            return _context.Set<Conta>().FirstOrDefault(x => x.ContaId == contaId);
        }
    }
}
