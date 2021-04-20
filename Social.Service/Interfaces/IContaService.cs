using Social.Model;
using Social.Model.Modelos;
using Social.Service.Models.Filtros;
using Social.Service.Models.Ordenacao;
using Social.Service.Models.Paginacao;
using System.Collections.Generic;

namespace Social.Service.Interfaces
{
    public interface IContaService
    {
        IEnumerable<ContaApi> RetornaListaConta();
        ContaPaginada RetornaListaContaPaginada(ContaFiltro filtro, ContaOrdem ordem, ContaPaginacao paginacao);
        Conta RetornaConta(string contaId);
        Conta CriarConta(ContaApi contaApi);
        Conta EditarConta(ContaApi contaApi);
        void ExcluirConta(Conta conta);
        void ExcluirPorContaId(string contaId);
        ContaApi RetornaContaApi(string contaId);
    }
}
