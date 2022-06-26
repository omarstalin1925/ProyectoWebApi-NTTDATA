using AutoMapper;
using ProyectoWebApi.BaseService;
using ProyectoWebApi.Dto;
using ProyectoWebApi.Entities;
using ProyectoWebApi.Repositories;

namespace ProyectoWebApi.Services
{
    public class CuentaService : ICuentaService

    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public CuentaService(
            ICuentaRepository cuentaRepository,
            IClienteRepository clienteRepository,
            IPersonaRepository personaRepository,
            IMovimientoRepository movimientoRepository,
            IMapper mapper,
            ApplicationDbContext applicationDbContext
            )

        {
            _cuentaRepository = cuentaRepository;
            _clienteRepository = clienteRepository;
            _personaRepository = personaRepository;
            _movimientoRepository = movimientoRepository;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }
        public async Task<int> CrearCuenta(CuentaDto cuentaDto)
        {
            var cli = await _clienteRepository.BuscarPorId(cuentaDto.ClienteId.Value);
            if (cli != null)
            {
                var insertCuenta = await _cuentaRepository.InsertarCuenta(_mapper.Map<Cuenta>(cuentaDto));
                return insertCuenta.CuentaId;
            }
            else
            {
                return 0;
            }

        }

        public async Task<int> EditarCuenta(CuentaDto cuentaDto)
        {
            var cta = await _cuentaRepository.BuscarPorId(cuentaDto.CuentaId);
            if (cta != null)
            {
                cta.NumCuenta = cuentaDto.NumCuenta;
                cta.SaldoInicial= cuentaDto.SaldoInicial;
                cta.Estado=cuentaDto.Estado;
                cta.TipoCuenta = cuentaDto.TipoCuenta;
                var editCuenta = await _cuentaRepository.EditarCuenta(cta);
                return editCuenta.CuentaId;
            }
            else
            {
                return 0;
            }
        }
        public async Task<bool> EliminarCuenta(string numCuenta)
        {
            var cta = await _cuentaRepository.ObtenerCuenta(numCuenta);
            var mov = await _movimientoRepository.BuscarPorIdCuenta(cta.CuentaId);
            if( mov.Count >0)
            {
                foreach (var item in mov) //eliminar todos los movimientos de esa cuenta
                {
                    await _movimientoRepository.EliminarMovimiento(item);
                }
            }
            if (cta!=null) // eliminar cuenta
            {
                await _cuentaRepository.EliminarCuenta(cta);
            }
           
            return true;
        }

        public async Task<CuentaDto> ObtenerCuenta(string numCuenta)
        {
            var cta = await _cuentaRepository.ObtenerCuenta(numCuenta);
            CuentaDto cuenta = _mapper.Map<CuentaDto>(cta);
            return cuenta;
        }

    }
}
