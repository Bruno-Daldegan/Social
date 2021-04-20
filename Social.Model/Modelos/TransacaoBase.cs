using System;
using System.ComponentModel.DataAnnotations;

namespace Social.Model.Modelos
{
    public abstract class TransacaoBase
    {
        [Key]
        public int Id { get; set; }
        public string Operacao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataOperacao { get; set; }
    }

    public abstract class TransacaoBaseApi
    {
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}
