namespace Social.Model.Modelos
{
    public class Debito : TransacaoBase
    {
        public string ContaDebito { get; set; }
    }

    public class DebitoApi : TransacaoBaseApi
    {
        public Debit Debit { get; set; }
    }
}
