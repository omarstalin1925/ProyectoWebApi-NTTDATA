using AutoMapper;
using ProyectoWebApi.BaseService;
using ProyectoWebApi.Dto;
using ProyectoWebApi.Entities;
using ProyectoWebApi.Repositories;

namespace ProyectoWebApi.Services
{
    public class MovimientoService : IMovimientoService

    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public MovimientoService(
            ICuentaRepository cuentaRepository,
            IClienteRepository clienteRepository,
            IPersonaRepository personaRepository,
            IMovimientoRepository movimientoRepository,
            IConfiguration configuration,
            IMapper mapper,
            ApplicationDbContext applicationDbContext
            )

        {
            _cuentaRepository = cuentaRepository;
            _clienteRepository = clienteRepository;
            _personaRepository = personaRepository;
            _movimientoRepository = movimientoRepository;
            _configuration = configuration;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }
        public async Task<ResultadoDto> CrearMovimiento(MovimientoDto movimientoDto)
        {

            decimal limiteDiario = _configuration.GetSection("Parametros").GetValue<int>("limiteRetiro"); ;

            ResultadoDto result = new ResultadoDto();
            var cta = await _cuentaRepository.BuscarPorId(movimientoDto.CuentaId);
            if (movimientoDto.TipoMovimiento == "Credito")
            {
                cta.SaldoInicial += movimientoDto.Valor.Value;
            }
            if (movimientoDto.TipoMovimiento == "Debito")
            {
                var listaDebitos = await _movimientoRepository.ObtenerMovimientoDebitoFechaHoy(movimientoDto.CuentaId);
                decimal totalDebDiario = 0;
                foreach (var deb in listaDebitos)
                {
                    totalDebDiario += deb.Valor.Value;
                }
                totalDebDiario += movimientoDto.Valor.Value;
                if (totalDebDiario < limiteDiario)
                {
                    if (cta.SaldoInicial == 0)
                    {
                        result.Mensaje = "Saldo no disponible";
                        return result;
                    }
                    else if (movimientoDto.Valor.Value > cta.SaldoInicial)
                    {
                        result.Mensaje = "Saldo insuficiente";
                        return result;
                    }
                    else
                    {
                        cta.SaldoInicial -= movimientoDto.Valor.Value;
                    }
                }
                else
                {
                    result.Mensaje = "Cupo diario Excedido";
                    return result;
                }

            }

            if (cta != null)
            {
                movimientoDto.Fecha = DateTime.Now;
                movimientoDto.Saldo = cta.SaldoInicial;
                var insertMovimiento = await _movimientoRepository.InsertarMovimiento(_mapper.Map<Movimiento>(movimientoDto));
                await _cuentaRepository.EditarCuenta(cta);//luego de crear el movimiento, actulizo el saldo de la cuenta
                result.Data = Convert.ToString(insertMovimiento.MovimientoId);
                result.Mensaje = "Movimiento creado";
                return result;
            }
            else
            {
                result.Mensaje = "No existe la cuenta";
                return result;
            }
        }

        public async Task<ResultadoDto> EditarMovimiento(MovimientoDto movimientoDto)
        {
            ResultadoDto resultado = new ResultadoDto();
            try
            {
                var mov = await _movimientoRepository.BuscarPorId(movimientoDto.MovimientoId);
                if (mov != null)
                {
                    mov.Fecha = movimientoDto.Fecha;
                    mov.Saldo = movimientoDto.Saldo;
                    mov.Valor = movimientoDto.Valor;
                    mov.TipoMovimiento = movimientoDto.TipoMovimiento;

                    var editMovimiento = await _movimientoRepository.EditarMovimiento(mov);
                    resultado.Data = Convert.ToString(editMovimiento.MovimientoId);
                    return resultado;
                }
                else
                {
                    resultado.Data = Convert.ToString(0);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                resultado.Mensaje=ex.Message.ToString();
                return resultado;
            }
            
        }

        public async Task<ResultadoDto> EliminarMovimiento(int id)
        {
            ResultadoDto resultado = new ResultadoDto();
            try
            {
                var mov = await _movimientoRepository.ObtenerMovimiento(id);
                var delMov = await _movimientoRepository.EliminarMovimiento(mov);
                resultado.Data = "1";
                resultado.Mensaje = "Eliminado Correctamente";
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message.ToString();
                return resultado;
            }
           
        }
        public async Task<List<ReporteMovimientoDto>> ObtenerListadoMovimientos(string fechaInicial, string fechaFinal, string identificacion)
        {
            List<ReporteMovimientoDto> listaReporte = new List<ReporteMovimientoDto>();
            var per = _personaRepository.ObtenerPersona(identificacion).Result;
            var cli = _clienteRepository.BuscarPorIdPersona(per.PersonaId).Result;
            var lisCta = _cuentaRepository.BuscarPorIdCliente(cli.ClienteId).Result;
            foreach (var cta in lisCta)
            {
                var listaMov = _movimientoRepository.BuscarPorIdCuenta(cta.CuentaId).Result;
                var filtroMovFecha = listaMov.Where(p => p.Fecha >= Convert.ToDateTime(fechaInicial) && p.Fecha <= Convert.ToDateTime(fechaFinal)).ToList();
                foreach (var mov in filtroMovFecha)
                {
                    ReporteMovimientoDto reporte = new ReporteMovimientoDto();
                    reporte.Cliente = per.Nombre;
                    reporte.NumeroCuenta = cta.NumCuenta;
                    reporte.Tipo = cta.TipoCuenta;
                    reporte.SaldoInicial = cta.SaldoInicial;
                    reporte.Estado = cta.Estado;
                    reporte.Fecha = mov.Fecha.Value.ToString();
                    reporte.Movimiento = mov.Valor.Value;
                    reporte.SaldoDisponible = mov.Saldo.Value;

                    listaReporte.Add(reporte);
                }
            }
            return listaReporte;
        }

        public async Task<ResultadoDto> ObtenerMovimiento(int id)
        {
            ResultadoDto resultado = new ResultadoDto();
            try
            {
                var mov = await _movimientoRepository.ObtenerMovimiento(id);
                MovimientoDto movimiento = _mapper.Map<MovimientoDto>(mov);
                resultado.Data = movimiento.ToString();
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message.ToString();
                return resultado;
            }

        }
    }
}
