using Social.Model;
using ET.FakeText;
using System;
using Social.Model.Modelos;

namespace Social.GeradorRandomico
{
    internal class GeradorAleatorioDeContas
    {

        private string NomeAleatorio()
        {
            var textGenerator = new TextGenerator();
            //textGenerator./*MaxSentenceLength*/ = 50;
            return textGenerator.GenerateWord(10);
        }

        private string DescricaoAleatoria()
        {
            var textGenerator = new TextGenerator(WordTypes.Name);
            //textGenerator.MaxSentenceLength = 100;
            return textGenerator.GenerateWord(20);
        }

        public Conta ContaAleatoria() 
        {
            return new Conta
            {
                ContaId = new Random().Next(10000, 99999).ToString(),
                Nome = NomeAleatorio(),
                Saldo = 100,
                Descricao = DescricaoAleatoria(),
                Status = true
            };
        }
    }
}