using Application.DTOs.Requests;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ReceivablesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AntecipacaoController : ControllerBase
    {
        private readonly ICalcularAntecipacaoService _antecipacaoService;

        public AntecipacaoController(ICalcularAntecipacaoService antecipacaoService)
        {
            _antecipacaoService = antecipacaoService;
        }

        [HttpPost("calcular")]
        public async Task<IActionResult> CalcularAntecipacao(string cnpj)
        {
            try
            {
                var response = await _antecipacaoService.CalcularAntecipacao(cnpj);

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao processar a solicitação.", detalhe = ex.Message });
            }
            
        }
    }
}

