using Social.Model;
using Social.Model.Modelos;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Social.Service.Models.Ordenacao
{
    public static class ContaOrdemExtensions
    {
        public static IQueryable<Conta> AplicaOrdenacao(this IQueryable<Conta> query, ContaOrdem ordem)
        {
            if ((ordem != null)&&(!string.IsNullOrEmpty(ordem.OrdenarPor)))
            {
                query = query.OrderBy(ordem.OrdenarPor);
            }
            return query;
        }
    }

    public class ContaOrdem
    {
        public string OrdenarPor { get; set; }
    }
}
