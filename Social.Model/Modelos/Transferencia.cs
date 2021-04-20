using System;

namespace Social.Model.Modelos
{
    public class Transferencia : TransacaoBase
    {
        public string ContaDebito { get; set; }
        public string ContaCredito { get; set; }
    }

    public class ExtratoApi
    {
        public DateTime Date { get; set; }
        public string Operation  { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
        public decimal Value { get; set; }
    }

    public class TransferenciaApi : TransacaoBaseApi
    {
        public Debit Debit { get; set; }
        public Credit Credit { get; set; }
    }

    public enum OrigemOperacao
    {
        Saque,
        Pagamento,
        Deposito,
        Transferencia,
        Invalid
    }
}
