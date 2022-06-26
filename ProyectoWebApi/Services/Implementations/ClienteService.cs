using AutoMapper;
using ProyectoWebApi.BaseService;
using ProyectoWebApi.Dto;
using ProyectoWebApi.Entities;
using ProyectoWebApi.Repositories;

namespace ProyectoWebApi.Services
{
    public class ClienteService : IClienteService

    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public ClienteService(
            IClienteRepository clienteRepository,
            IPersonaRepository personaRepository,
            ICuentaRepository cuentaRepository,
            IMovimientoRepository movimientoRepository,
            IMapper mapper,
            ApplicationDbContext applicationDbContext
            )

        {
            _clienteRepository = clienteRepository;
            _personaRepository = personaRepository;
            _cuentaRepository = cuentaRepository;
            _movimientoRepository = movimientoRepository;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> CrearCliente(UsuarioDto usuarioDto)
        {
            var existePer = await _personaRepository.BuscarPorIdentificacion(usuarioDto.Identificacion);

            if (existePer == null)
            {
                var insertPersona = await _personaRepository.InsertarPersona(_mapper.Map<Persona>(usuarioDto));
                Cliente cliente = _mapper.Map<Cliente>(usuarioDto);
                if (insertPersona != null)
                {
                    cliente.PersonaId = insertPersona.PersonaId;
                }
                var insertCliente = await _clienteRepository.InsertarCliente(cliente);
                return insertCliente.ClienteId;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> EditarCliente(UsuarioDto usuarioDto)
        {
            var existePer = await _personaRepository.BuscarPorIdentificacion(usuarioDto.Identificacion);

            if (existePer != null)
            {
                Persona per = _mapper.Map<Persona>(existePer);
                per.Nombre = usuarioDto.Nombre;
                per.Edad = usuarioDto.Edad;
                per.Identificacion = usuarioDto.Identificacion;
                per.Telefono = usuarioDto.Telefono;
                per.Genero = usuarioDto.Genero;
                var editPersona = await _personaRepository.EditarPersona(per);

                var cli = await _clienteRepository.BuscarPorIdPersona(editPersona.PersonaId);

                Cliente cliente = _mapper.Map<Cliente>(cli);
                cli.Contrasenia = usuarioDto.Contrasenia;
                cli.Estado = usuarioDto.Estado;

                var editCliente = await _clienteRepository.EditarCliente(cliente);
                return editCliente.ClienteId;
            }
            else
            {
                return 0;
            }
        }

        public async Task<bool> EliminarCliente(string identificacion)
        {
            var per = await _personaRepository.ObtenerPersona(identificacion);
            var cli = await _clienteRepository.BuscarPorIdPersona(per.PersonaId);
            var cta = await _cuentaRepository.BuscarPorIdCliente(cli.ClienteId);
            if (cta.Count > 0)
            {
                foreach (var item in cta) //eliminar cuenta
                {
                    var listMov = await _movimientoRepository.BuscarPorIdCuenta(item.CuentaId);
                    if (listMov.Count>0)
                    {
                        foreach (var movto in listMov) //eliminar todos los movimientos
                        {
                            await _movimientoRepository.EliminarMovimiento(movto);
                        }
                    }
                    await _cuentaRepository.EliminarCuenta(item);
                }
            }
            if (cli != null) //eliminar cliente
            {
                await _clienteRepository.EliminarCliente(cli);
            }
            if (per != null) //eliminar persona
            {
                await _personaRepository.EliminarPersona(per);
            }

            return true;
        }

        public async Task<PersonaDto> ObtenerCliente(string identificacion)
        {
            var per = await _personaRepository.ObtenerPersona(identificacion);
            PersonaDto usuario = _mapper.Map<PersonaDto>(per);
            return usuario;
        }
    }
}
