using Microsoft.AspNetCore.Mvc;
using ProyectoWebApi.Dto;
using ProyectoWebApi.Services;

namespace  ProyectoWebApi.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;
       
        public CuentaController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpPost("crearCuenta")]
        public async Task<IActionResult> CrearCuenta(CuentaDto cuentaDto)
        {
            var cuenta = await _cuentaService.CrearCuenta(cuentaDto);
            return Ok(cuenta);
        }

        [HttpPut("editarCuenta")]
        public async Task<IActionResult> EditarCuenta(CuentaDto cuentaDto)
        {
            var cuenta = await _cuentaService.EditarCuenta(cuentaDto);
            return Ok(cuenta);
        }

        [HttpGet("obtenerCuenta")]
        public async Task<IActionResult> ObtenerCuenta(string numCuenta)
        {
            var cuenta = await _cuentaService.ObtenerCuenta(numCuenta);
            return Ok(cuenta);
        }

        [HttpDelete("eliminarCuenta")]
        public async Task<IActionResult> EliminarCuenta(string numCuenta)
        {
            var cuenta = await _cuentaService.EliminarCuenta(numCuenta);
            return Ok(cuenta);
        }
    }
}
