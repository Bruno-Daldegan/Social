using Social.Model;
using Social.Model.Modelos;
using System;
using System.Linq;

namespace Social.Service.Models.Filtros
{
    public static class TrasnferenciaFiltroExtensions
    {
        public static IQueryable<Transferencia> AplicaFiltro(this IQueryable<Transferencia> query, TransferenciaFiltro filtro)
        {
            if (filtro != null)
            {
                if (!string.IsNullOrEmpty(filtro.Operacao))
                {
                    query = query.Where(l => l.Operacao.Equals(filtro.Operacao));
                }
                if (filtro.DataOperacao != null && filtro.DataOperacao.Year > 1753)
                {
                    query = query.Where(l => l.DataOperacao.Date.Equals(filtro.DataOperacao.Date));
                }
            }
            return query;
        }
    }

    public class TransferenciaFiltro
    {
        public string Operacao { get; set; }
        public DateTime DataOperacao { get; set; }
    }
}
