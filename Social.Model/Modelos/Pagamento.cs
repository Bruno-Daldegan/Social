using System;

namespace Social.Model.Modelos
{
    public class RetornoPagamento
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }

    public class PagamentoApi
    {
        public string Barcode { get; set; }
        public DateTime Expiration_date { get; set; }
        public decimal Amount { get; set; }
    }
}
