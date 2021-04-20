using Social.DAL.Contas;
using Social.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Social.Model.Modelos;

namespace Social.GeradorRandomico
{
    class Program
    {
        static void Main(string[] args)
        {
            GerarContas();
        }

        private static void GerarContas()
        {
            var gerador = new GeradorAleatorioDeContas();
            var contas = new List<Conta>();

            Console.WriteLine("Gerando contas aleatórias...");
            for (int i = 0; i < 10; i++)
            {
                contas.Add(gerador.ContaAleatoria());
            }

            //... e depois persistir essa lista
            Console.WriteLine("Persistindo a lista...");
            var optionsBuilder = new DbContextOptionsBuilder<SocialContext>();
            optionsBuilder
                .UseSqlServer("Server=localhost;Database=SOCIAL;Trusted_Connection=True;MultipleActiveResultSets=true");

            using (var ctx = new SocialContext(optionsBuilder.Options))
            {
                ctx.Contas.AddRange(contas);
                ctx.SaveChanges();
            }
        }
    }
}
