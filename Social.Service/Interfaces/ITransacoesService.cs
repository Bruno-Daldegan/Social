using Social.Model;
using Social.Model.Modelos;
using Social.Service.Models.Filtros;
using Social.Service.Models.Ordenacao;
using Social.Service.Models.Paginacao;

namespace Social.Service.Interfaces
{
    public interface ITransacoesService
    {
        void TransferenciaInterna(TransferenciaApi transferencia, OrigemOperacao origem);
        void Debito(Transferencia transferencia);
        void Debito(DebitoApi saque, OrigemOperacao origem);
        void Credito(CreditoApi transferencia, OrigemOperacao origem);
        void Credito(Transferencia transferencia);
        RetornoPagamento EfetuaPagamento(PagamentoApi pagamento);
        ExtratoPaginado RetornaExtrato(string contaId, TransferenciaFiltro filtro, TransferenciaOrdem ordem, TransferenciaPaginacao paginacao);
        bool TemTransacoesVinculadas(string contaId);
    }
}
