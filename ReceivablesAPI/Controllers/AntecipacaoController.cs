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
            var response = await _antecipacaoService.CalcularAntecipacao(cnpj);

            return Ok(response);
        }
    }
}

