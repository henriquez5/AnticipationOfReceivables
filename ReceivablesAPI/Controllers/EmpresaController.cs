using Application.DTOs.Requests;
using Application.DTOs.Responses;
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
            var response = await _empresaService.ObterTodasEmpresas();

            return Ok(response);
        }

        [HttpGet("obter-todas-id")]
        public async Task<IActionResult> ObterEmpresaPorId(int id)
        {
            var response = await _empresaService.ObterEmpresaPorId(id);

            return Ok(response);
        }

        [HttpPost("inserir-empresa")]
        public async Task<IActionResult> InserirEmpresa(CriarEmpresaRequest criarEmpresa)
        {
            var response = await _empresaService.InserirEmpresa(criarEmpresa);

            return Ok(response);
        }
    }
}
