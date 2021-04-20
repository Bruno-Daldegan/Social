using Social.Model;
using Social.Model.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Social.Service.Models.Paginacao
{
    public class TransferenciaPaginacao
    {
        private readonly int TAMANHO_PADRAO = 25;
        private int _pagina = 0;
        private int _tamanho = 0;

        public int Pagina
        {
            get
            {
                return (_pagina <= 0) ? 1 : _pagina;
            }
            set
            {
                _pagina = value;
            }
        }
        public int Tamanho
        {
            get
            {
                return (_tamanho <= 0) ? TAMANHO_PADRAO : _tamanho;
            }
            set
            {
                _tamanho = value;
            }
        }

        public int QtdeParaDescartar => Pagina > 0 ? Tamanho * (Pagina - 1) : Tamanho;
    }
    public class ExtratoPaginado
    {
        public IList<ExtratoApi> Resultado { get; set; }
        public int Total { get; set; }
        public int TotalPaginas { get; set; }
        public int NumeroPagina { get; set; }
        public int TamanhoPagina { get; set; }
        public string Anterior { get; set; }
        public string Proximo { get; set; }

        public static ExtratoPaginado From(TransferenciaPaginacao parametros, IQueryable<ExtratoApi> origem, string contaId)
        {
            if (parametros == null)
            {
                parametros = new TransferenciaPaginacao();
            }
            int totalItens = origem.Count();
            //260 itens / 25 itens por página >> 10,4 e seu teto é 11.
            int totalPaginas = (int)Math.Ceiling(totalItens / (double)parametros.Tamanho);
            bool temPaginaAnterior = (parametros.Pagina > 1);
            bool temProximaPagina = (parametros.Pagina < totalPaginas);
            return new ExtratoPaginado
            {
                Total = totalItens,
                TotalPaginas = totalPaginas,
                TamanhoPagina = parametros.Tamanho,
                NumeroPagina = parametros.Pagina,
                Resultado = totalItens > 0 ? origem.Skip(parametros.QtdeParaDescartar).Take(parametros.Tamanho).ToList() : new List<ExtratoApi>(),
                Anterior = temPaginaAnterior
                    ? $"extrato/{contaId}?pagina={parametros.Pagina - 1}&tamanho={parametros.Tamanho}"
                    : "",
                Proximo = temProximaPagina
                    ? $"extrato/{contaId}?pagina={parametros.Pagina + 1}&tamanho={parametros.Tamanho}"
                    : ""
            };
        }
    }
}
