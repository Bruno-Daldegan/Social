using System.ComponentModel.DataAnnotations;

namespace Social.Model.Modelos
{
    public class Conta
    {
        [Key]
        public int Id { get; set; }
        public string ContaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Saldo { get; set; }
        public bool Status { get; set; }
    }

    public class ContaApi
    {
        public string Idenfifier { get; set; }
        /// <summary>
        /// Nome do titular.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Descricao da conta.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Status da conta.
        /// </summary>
        public string Status { get; set; }
    }
}
