using Application.DTOs.Requests;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ReceivablesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaFiscalController : ControllerBase
    {
        private readonly INotaFiscalService _notaFiscalService;

        public NotaFiscalController(INotaFiscalService notaFiscalService)
        {
            _notaFiscalService = notaFiscalService;
        }

        [HttpGet("obter-todas")]
        public async Task<IActionResult> ObterTodasNotasFiscais()
        {
            var response = await _notaFiscalService.ObterTodasNotasFiscais();

            return Ok(response);
        }

        [HttpGet("obter-todas-cnpj")]
        public async Task<IActionResult> ObterTodasNotasFiscaisPorCNPJ(string cnpj)
        {
            var response = await _notaFiscalService.ObterTodasNotasFiscaisPorCNPJ(cnpj);

            return Ok(response);
        }

        [HttpPost("inserir-nota-fiscal")]
        public async Task<IActionResult> InserirNotaFiscal(CriarNotaFiscalRequest notafiscal)
        {
            var response = await _notaFiscalService.InserirNotaFiscal(notafiscal);

            return Ok(response);
        }
    }
}
