using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Social.DAL.Contas;
using Social.Model.Modelos;
using Social.RestFullAPI.Controllers;
using Social.Service.Services;
using System;
using Xunit;

namespace Social.Tests
{
    public class ContaControllerEndpointIncluir
    {
        [Fact]
        public void AoCadastrarContaApiDeveRetornar201()
        {
            //arrange
            var options = new DbContextOptionsBuilder<SocialContext>()
                .UseInMemoryDatabase("SocialContext")
                .Options;

            var contexto = new SocialContext(options);

            var repoConta = new ContaRepository(contexto);
            var repoTransf = new TransferenciaRepository(contexto);

            var contaService = new ContaService(repoConta);
            var transacoesService = new TransacoesService(repoTransf, repoConta);
            
            var controlador = new ContasController(contaService, transacoesService);

            var model = new ContaApi
            {
                Name = "Bruno",
                Description = "Daldegan",
                Status = "ACTIVE",
                Idenfifier = "65432"
            };

            //act
            var retorno = controlador.Incluir(model);

            Assert.IsType<CreatedResult>(retorno); //201
        }

        [Fact]
        public void QuandoExcecaoForLancadaDeveRetornarStatusCode400()
        {
            //arrange
            var mockConta = new Mock<IContaRepository>();
            mockConta.Setup(r => r.Incluir(It.IsAny<Conta[]>())).Throws(new Exception("Houve um erro"));
            
            var repoConta = mockConta.Object;

            var mockTransf = new Mock<ITransferenciaRepository>().Object;

            var transService = new TransacoesService(mockTransf, repoConta);

            var contaService = new ContaService(repoConta);

            var controlador = new ContasController(contaService, transService);

            var model = new ContaApi();
            model.Name = "Bruno";
            model.Description = "Daldegan";
            model.Status = "ACTIVE";
            model.Idenfifier = "65432";

            //act
            var retorno = controlador.Incluir(model);

            //assert
            Assert.IsType<BadRequestObjectResult>(retorno);

            var statusCodeRetornado = (retorno as BadRequestObjectResult).StatusCode;
            Assert.Equal(400, statusCodeRetornado);
        }
    }
}
