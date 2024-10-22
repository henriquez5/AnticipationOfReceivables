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
            try
            {
                var response = await _notaFiscalService.ObterTodasNotasFiscais();

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet("obter-todas-cnpj")]
        public async Task<IActionResult> ObterTodasNotasFiscaisPorCNPJ(string cnpj)
        {
            try
            {
                var response = await _notaFiscalService.ObterTodasNotasFiscaisPorCNPJ(cnpj);

                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost("inserir-nota-fiscal")]
        public async Task<IActionResult> InserirNotaFiscal(CriarNotaFiscalRequest notafiscal)
        {
            try
            {
                var response = await _notaFiscalService.InserirNotaFiscal(notafiscal);

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
