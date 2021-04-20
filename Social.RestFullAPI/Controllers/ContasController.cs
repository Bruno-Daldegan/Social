using Microsoft.AspNetCore.Mvc;
using Social.Model.Modelos;
using Social.Model.Extensoes;
using Social.Service.Interfaces;
using Social.Service.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Social.RestFullAPI.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1.0/contas")]
    public class ContasController : ControllerBase
    {
        private readonly IContaService _contaService;
        private readonly ITransacoesService _transferenciaService;
        public ContasController(IContaService contaService, ITransacoesService transferenciaService)
        {
            _contaService = contaService;
            _transferenciaService = transferenciaService;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(List<ContaApi>))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult ListaDeContas()
        {
            try
            {
                var lista = _contaService.RetornaListaConta();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{contaId}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(ContaApi))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Recuperar(string contaId)
        {
            try
            {
                var conta = _contaService.RetornaContaApi(contaId);
                if (conta == null)
                {
                    return NotFound();
                }
                return Ok(conta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 201, Type = typeof(ContaApi))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Incluir([FromBody] ContaApi model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conta = _contaService.CriarConta(model);
                    return Created($"contas/{model.Idenfifier}", conta.ToApi());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(statusCode: 200, Type = typeof(ContaApi))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Alterar([FromBody] ContaApi model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conta = _contaService.EditarConta(model);
                    return Ok(conta.ToApi());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{contaId}")]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 203)]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 400, Type = typeof(ErroResponse))]
        public IActionResult Remover(string contaId)
        {
            try
            {
                var conta = _contaService.RetornaConta(contaId);
                if (conta == null)
                {
                    return NotFound();
                }

                if(_transferenciaService.TemTransacoesVinculadas(contaId))
                {
                    return BadRequest("Não foi possível remover conta, pois existem transações vinculadas a ela.");
                }

                _contaService.ExcluirConta(conta);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("saldo/{contaId}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(string))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult RetornaSaldo(string contaId)
        {
            try
            {
                var conta = _contaService.RetornaConta(contaId);
                if (conta == null)
                {
                    return NotFound();
                }

                var retorno = $"O saldo de {conta.Nome} ({conta.Descricao}) é {conta.Saldo.ToString("C2")}.";
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}