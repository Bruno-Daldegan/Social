using Microsoft.AspNetCore.Mvc;
using Social.Service.Models;
using Social.Service.Models.Filtros;
using Social.Service.Models.Ordenacao;
using Social.Service.Models.Paginacao;
using Social.Service.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Social.RestFullAPI.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v2.0/contas")]
    public class Contas2Controller : ControllerBase
    {
        private readonly IContaService _contaService;

        public Contas2Controller(IContaService contaService)
        {
            _contaService = contaService;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(ContaPaginada))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult ListaDeContas(
            [FromQuery] ContaFiltro filtro,
            [FromQuery] ContaOrdem ordem,
            [FromQuery] ContaPaginacao paginacao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contas = _contaService.RetornaListaContaPaginada(filtro, ordem, paginacao);
                    if (contas == null)
                        return NotFound();

                    return Ok(contas);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest();
        }

        [HttpPatch("cancelar/{contaId}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(bool))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult InativarConta(string contaId)
        {
            try
            {
                var conta = _contaService.RetornaContaApi(contaId);
                if (conta == null)
                {
                    return NotFound();
                }

                conta.Status = "INATIVE";

                var contaInativa = _contaService.EditarConta(conta);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("ativar/{contaId}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(bool))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErroResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult AtivarConta(string contaId)
        {
            try
            {
                var conta = _contaService.RetornaContaApi(contaId);
                if (conta == null)
                {
                    return NotFound();
                }

                conta.Status = "ACTIVE";

                var contaInativa = _contaService.EditarConta(conta);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}