using Social.Model;
using Social.Model.Modelos;
using System.Linq;

namespace Social.Service.Models.Filtros
{
    public static class ContaFiltroExtensions
    {
        public static IQueryable<Conta> AplicaFiltro(this IQueryable<Conta> query, ContaFiltro filtro)
        {
            if (filtro != null)
            {
                if (!string.IsNullOrEmpty(filtro.Nome))
                {
                    query = query.Where(l => l.Nome.Contains(filtro.Nome));
                }
                if (!string.IsNullOrEmpty(filtro.Descricao))
                {
                    query = query.Where(l => l.Nome.Contains(filtro.Descricao));
                }
                if (filtro.Status.HasValue)
                {
                    var filtroS = filtro.Status.Value;
                    query = query.Where(l => l.Status.Equals(filtroS));
                }
            }
            return query;
        }
    }

    public class ContaFiltro
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool? Status { get; set; }
    }
}
