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

                if (response == null || !response.Any())
                {
                    return Ok("Nenhuma nota fiscal cadastrada.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao processar a solicitação.", detalhe = ex.Message });
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
            catch (FormatException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao processar a solicitação.", detalhe = ex.Message });
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
            catch (FormatException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao processar a solicitação.", detalhe = ex.Message });
            }

        }

        [HttpDelete("deletar-nota")]
        public async Task<IActionResult> DeletarNotaFiscal(int id)
        {
            try
            {
                var response = await _notaFiscalService.DeletarNotaFiscal(id);

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
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
