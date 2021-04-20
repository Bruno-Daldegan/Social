namespace Social.Model.Modelos
{
    public class Credito : TransacaoBase
    {
        public string ContaCredito { get; set; }
    }

    public class CreditoApi : TransacaoBaseApi
    {
        public Credit Credit { get; set; }
    }
}
