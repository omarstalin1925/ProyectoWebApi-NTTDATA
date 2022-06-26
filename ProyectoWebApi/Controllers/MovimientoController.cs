using Microsoft.AspNetCore.Mvc;
using ProyectoWebApi.Dto;
using ProyectoWebApi.Services;

namespace  ProyectoWebApi.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoService _movimientoService;
        
        public MovimientoController(IMovimientoService movimientoService)
        {
            _movimientoService = movimientoService;
        }

        [HttpPost("crearMovimiento")]
        public async Task<IActionResult> CrearMovimiento(MovimientoDto movimientoDto)
        {
            var movimiento = await _movimientoService.CrearMovimiento(movimientoDto);
            return Ok(movimiento);
        }

        [HttpPut("editarMovimiento")]
        public async Task<IActionResult> EditarMovimiento(MovimientoDto movimientoDto)
        {
            var movimiento = await _movimientoService.EditarMovimiento(movimientoDto);
            return Ok(movimiento);
        }

        [HttpGet("obtenerMovimiento")]
        public async Task<IActionResult> ObtenerMovimiento(int id)
        {
            var movimiento = await _movimientoService.ObtenerMovimiento(id);
            return Ok(movimiento);
        }

        [HttpDelete("eliminarMovimiento")]
        public async Task<IActionResult> EliminarMovimiento(int id)
        {
            var movimiento = await _movimientoService.EliminarMovimiento(id);
            return Ok(movimiento);
        }

        [HttpGet("obtenerListadoMovimientos")]
        public async Task<IActionResult> ObtenerListadoMovimientos(string fechaInicial,string fechaFinal, string identificacion)
        {
            var movimiento = await _movimientoService.ObtenerListadoMovimientos(fechaInicial, fechaFinal, identificacion);
            return Ok(movimiento);
        }
    }
}
