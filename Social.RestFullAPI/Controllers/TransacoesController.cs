using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social.Model;
using Social.Model.Modelos;
using Social.Service.Interfaces;
using Social.Service.Models;
using Social.Service.Models.Filtros;
using Social.Service.Models.Ordenacao;
using Social.Service.Models.Paginacao;
using System;

namespace Social.RestFullAPI.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly ITransacoesService _transferenciaService;

        public TransacoesController(ITransacoesService transferenciaService)
        {
            _transferenciaService = transferenciaService;
        }

        [HttpPost("deposito")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Deposito([FromBody] CreditoApi credito)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _transferenciaService.Credito(credito, OrigemOperacao.Deposito);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }

        [HttpPost("saque")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Saque([FromBody] DebitoApi debito)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _transferenciaService.Debito(debito, OrigemOperacao.Saque);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }

        [HttpPost("transferencia")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Transferir([FromBody] TransferenciaApi transferencia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _transferenciaService.TransferenciaInterna(transferencia, OrigemOperacao.Transferencia);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }

        [HttpPost("pagamento")]
        [ProducesResponseType(statusCode: 200, Type = typeof(RetornoPagamento))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Pagamento([FromBody] PagamentoApi pagamento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var retorno = _transferenciaService.EfetuaPagamento(pagamento);

                    return Ok(retorno);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }

        [HttpGet("extrato/{contaId}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(ExtratoPaginado))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult RetornaExtrato(string contaId,
            [FromQuery] TransferenciaFiltro filtro,
            [FromQuery] TransferenciaOrdem ordem, 
            [FromQuery] TransferenciaPaginacao paginacao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var extrato = _transferenciaService.RetornaExtrato(contaId, filtro, ordem, paginacao);
                    if (extrato == null)
                        return NotFound();

                    return Ok(extrato);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest();
        }
    }
}