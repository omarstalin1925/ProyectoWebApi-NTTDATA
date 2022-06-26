using Microsoft.AspNetCore.Mvc;
using ProyectoWebApi.Dto;
using ProyectoWebApi.Services;

namespace  ProyectoWebApi.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
       
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost("crearCliente")]
        public async Task<IActionResult> CrearCliente(UsuarioDto usuarioDto)
        {
            var cliente = await _clienteService.CrearCliente(usuarioDto);
            return Ok(cliente);
        }

        [HttpPut("editarCliente")]
        public async Task<IActionResult> EditarCliente(UsuarioDto usuarioDto)
        {
            var cliente = await _clienteService.EditarCliente(usuarioDto);
            return Ok(cliente);
        }

        [HttpGet("obtenerCliente")]
        public async Task<IActionResult> ObtenerCliente(string identificacion)
        {
            var cliente = await _clienteService.ObtenerCliente(identificacion);
            return Ok(cliente);
        }

        [HttpDelete("eliminarCliente")]
        public async Task<IActionResult> EliminarCliente(string identificacion)
        {
            var cliente = await _clienteService.EliminarCliente(identificacion);
            return Ok(cliente);
        }
    }
}
