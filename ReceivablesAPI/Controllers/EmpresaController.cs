using Application.DTOs.Requests;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ReceivablesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet("obter-todas")]
        public async Task<IActionResult> ObterTodasEmpresas()
        {
            try
            {
                var response = await _empresaService.ObterTodasEmpresas();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao processar a solicitação.", detalhe = ex.Message });
            }

        }

        [HttpGet("obter-por-id")]
        public async Task<IActionResult> ObterEmpresaPorId(int id)
        {
            try
            {
                var response = await _empresaService.ObterEmpresaPorId(id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao processar a solicitação.", detalhe = ex.Message });
            }
        }

        [HttpPost("inserir-empresa")]
        public async Task<IActionResult> InserirEmpresa(CriarEmpresaRequest criarEmpresa)
        {
            try
            {
                var response = await _empresaService.InserirEmpresa(criarEmpresa);

                return Ok(response);
            }
            catch (ArgumentException ex)
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
