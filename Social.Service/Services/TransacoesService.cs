using Social.DAL.Contas;
using Social.Model.Extensoes;
using Social.Model.Modelos;
using Social.Service.Interfaces;
using Social.Service.Models.Filtros;
using Social.Service.Models.Ordenacao;
using Social.Service.Models.Paginacao;
using System;
using System.Linq;

namespace Social.Service.Services
{
    public class TransacoesService : ITransacoesService
    {
        private readonly ITransferenciaRepository _repoTransf;
        private readonly IContaRepository _repoContaf;
        public TransacoesService(ITransferenciaRepository repoTransf, IContaRepository repoConta)
        {
            _repoTransf = repoTransf;
            _repoContaf = repoConta;
        }

        public void Debito(Transferencia transferencia)
        {
            var contaDebito = _repoContaf.FindByConta(transferencia.ContaDebito);

            ValidaDebito(transferencia.Valor, contaDebito);

            contaDebito.Saldo -= transferencia.Valor;

            _repoContaf.Alterar(contaDebito);

            RegistraTransacao(transferencia);
        }

        public void Debito(DebitoApi debito, OrigemOperacao origem)
        {
            var contaDebito = _repoContaf.FindByConta(debito.Debit.Idenfifier);
            
            if(origem != OrigemOperacao.Pagamento)
                ValidaDebito(debito.Value, contaDebito);

            contaDebito.Saldo -= debito.Value;

            _repoContaf.Alterar(contaDebito);

            RegistraTransacao(debito.ToTransferencia(origem));
        }

        public void Credito(Transferencia transferencia)
        {
            var contaCredito = _repoContaf.FindByConta(transferencia.ContaCredito);

            ValidaCredito(contaCredito);

            contaCredito.Saldo += transferencia.Valor;

            _repoContaf.Alterar(contaCredito);
        }

        public void Credito(CreditoApi deposito, OrigemOperacao origem)
        {
            var contaCredito = _repoContaf.FindByConta(deposito.Credit.Idenfifier);

            if (contaCredito == null)
            {
                throw new Exception("Conta crédito  não encontrada!");
            }

            contaCredito.Saldo += deposito.Value;

            _repoContaf.Alterar(contaCredito);

            RegistraTransacao(deposito.ToTransferencia(origem));
        }

        public void TransferenciaInterna(TransferenciaApi transferenciaApi, OrigemOperacao origem)
        {
            var transferencia = transferenciaApi.ToModel(origem);
            Debito(transferencia);
            Credito(transferencia);
        }

        private void RegistraTransacao(Transferencia transferencia)
        {
            _repoTransf.Incluir(transferencia);
        }

        public RetornoPagamento EfetuaPagamento(PagamentoApi pagamento)
        {
            var dataOperacao = DateTime.Now;

            var contaValida = ValidaBoleto(pagamento, dataOperacao);

            Debito(new DebitoApi
            {
                Date = dataOperacao,
                Value = pagamento.Amount,
                Debit = new Debit
                {
                    Idenfifier = contaValida.ContaId,
                    Name = contaValida.Nome
                }
            }, OrigemOperacao.Pagamento);

            return new RetornoPagamento
            {
                Name = contaValida.Nome,
                Value = pagamento.Amount,
                Date = dataOperacao
            };
        }

        public ExtratoPaginado RetornaExtrato(string contaId, TransferenciaFiltro filtro,
            TransferenciaOrdem ordem, TransferenciaPaginacao paginacao)
        {
            var lista = _repoTransf.GetTransacoesPorContaId(contaId)
                        .AplicaFiltro(filtro)
                        .AplicaOrdenacao(ordem)
                        .Select(l => l.ToApi());

            return ExtratoPaginado.From(paginacao, lista, contaId);
        }

        /// <summary>
        /// Padrão fictício de codBarras:
        /// Da posição 0-4: representam o numero da conta.
        /// Da posição 5-12 representam a data de emissao.
        /// Da posição 13-20 representam a data de vencimento.
        /// Da posição 21-34 representam o cpf/cnpf do favorecido.
        /// Da posição 35-44 representam o valor do boleto.
        /// </summary>
        /// <param name="pagamento"></param>
        /// <param name="dataOperacao"></param>
        /// <returns></returns>
        private Conta ValidaBoleto(PagamentoApi pagamento, DateTime dataOperacao)
        {
            var contaId = pagamento.Barcode.Substring(0, 5);
            var dataVencimento = pagamento.Barcode.Substring(13, 8);
            var valor = pagamento.Barcode.Substring(35, 10);

            var conta = _repoContaf.FindByConta(contaId);

            if (conta == null)
            {
                throw new Exception("Conta inválida!");
            }

            if (!conta.Status)
            {
                throw new Exception("Conta inativa!");
            }

            var valorBoleto = Convert.ToInt32(valor) * 0.01;

            if(conta == null || Convert.ToDecimal(valorBoleto) != pagamento.Amount)
            {
                throw new Exception("Boleto inválido!");
            }

            var dia = Convert.ToInt32(dataVencimento.Substring(0, 2));
            var mes = Convert.ToInt32(dataVencimento.Substring(2, 2));
            var ano = Convert.ToInt32(dataVencimento.Substring(4, 4));

            var dataExtraida = new DateTime(ano, mes, dia);

            if (dataExtraida != pagamento.Expiration_date.Date)
            {
                throw new Exception("Data inválida!");
            }

            if (dataOperacao.Date > pagamento.Expiration_date.Date)
            {
                throw new Exception("Boleto vencido!");
            }

            if (conta.Saldo < pagamento.Amount)
            {
                throw new Exception("Saldo da conta insuficiente!");
            }

            return conta;
        }

        private void ValidaDebito(decimal valor, Conta contaDebito)
        {
            if (contaDebito == null)
            {
                throw new Exception("Conta débito não encontrada!");
            }

            if (!contaDebito.Status)
            {
                throw new Exception("Conta inativa!");
            }

            if (contaDebito.Saldo < valor)
            {
                throw new Exception("Saldo da conta insuficiente!");
            }
        }

        private void ValidaCredito(Conta contaCredito)
        {
            if (contaCredito == null)
            {
                throw new Exception("Conta crédito  não encontrada!");
            }

            if (!contaCredito.Status)
            {
                throw new Exception("Conta crédito inativa!");
            }
        }

        public bool TemTransacoesVinculadas(string contaId)
        {
            return _repoTransf.GetTransacoesPorContaId(contaId).Any();
        }
    }
}
