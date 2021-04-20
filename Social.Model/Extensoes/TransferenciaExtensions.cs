using Social.Model.Modelos;

namespace Social.Model.Extensoes
{
    public static class TransferenciaExtensions
    {
        public static DebitoApi ToDebitoApi(this TransferenciaApi model)
        {
            return new DebitoApi
            {
                Date = model.Date,
                Value = model.Value,
                Debit = new Debit
                {
                    Idenfifier = model.Debit.Idenfifier,
                    Name = model.Debit.Name
                }
            };
        }

        public static CreditoApi ToCreditoApi(this TransferenciaApi model)
        {
            return new CreditoApi
            {
                Date = model.Date,
                Value = model.Value,
                Credit = new Credit
                {
                    Idenfifier = model.Credit.Idenfifier,
                    Name = model.Credit.Name
                }
            };
        }

        public static Transferencia ToTransferencia(this DebitoApi model, OrigemOperacao origem)
        {
            return new Transferencia
            {
                ContaDebito = model.Debit.Idenfifier,
                Operacao = RetornaTipoOperacao(origem),
                DataOperacao = model.Date,
                Valor = model.Value
            };
        }

        public static Transferencia ToTransferencia(this CreditoApi model, OrigemOperacao origem)
        {
            return new Transferencia
            {
                ContaCredito = model.Credit.Idenfifier,
                Operacao = RetornaTipoOperacao(origem),
                DataOperacao = model.Date,
                Valor = model.Value
            };
        }

        public static Transferencia ToModel(this TransferenciaApi model, OrigemOperacao origem)
        {
            return new Transferencia
            {
                ContaCredito = model.Credit.Idenfifier,
                ContaDebito = model.Debit.Idenfifier,
                Operacao = RetornaTipoOperacao(origem),
                DataOperacao = model.Date,
                Valor = model.Value
            };
        }

        private static string RetornaTipoOperacao(OrigemOperacao origemOperacao)
        {
            switch (origemOperacao)
            {
                case OrigemOperacao.Saque: return "S";     
                case OrigemOperacao.Deposito: return "D";   
                case OrigemOperacao.Pagamento: return "P";   
                case OrigemOperacao.Transferencia: return "T";   
            }
            return null;
        }

        private static string RetornaTipoOperacaoEnum(string origemOperacao)
        {
            switch (origemOperacao)
            {
                case "S": return OrigemOperacao.Saque.ToString();
                case "D": return OrigemOperacao.Deposito.ToString();
                case "P": return OrigemOperacao.Pagamento.ToString();
                case "T": return OrigemOperacao.Transferencia.ToString();
            }
            return null;
        }

        public static ExtratoApi ToApi(this Transferencia transferencia)
        {
            return new ExtratoApi
            {
                Date = transferencia.DataOperacao,
                Value = transferencia.Valor,
                Operation = RetornaTipoOperacaoEnum(transferencia.Operacao),
                Credit = transferencia.ContaCredito,
                Debit = transferencia.ContaDebito
            };
        }
    }

    public enum TipoOperacao
    {
        Invalid,
        Debito,
        Credito,
        TransferenciaEntreContas
    }

}
