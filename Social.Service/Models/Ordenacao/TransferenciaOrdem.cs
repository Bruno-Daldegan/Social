using Social.Model;
using Social.Model.Modelos;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Social.Service.Models.Ordenacao
{
    public static class TransferenciaOrdemExtensions
    {
        public static IQueryable<Transferencia> AplicaOrdenacao(this IQueryable<Transferencia> query, TransferenciaOrdem ordem)
        {
            if ((ordem != null)&&(!string.IsNullOrEmpty(ordem.OrdenarPor)))
            {
                query = query.OrderBy(ordem.OrdenarPor);
            }
            return query;
        }
    }

    public class TransferenciaOrdem
    {
        public string OrdenarPor { get; set; }
    }
}
