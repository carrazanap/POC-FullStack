using Application.ApplicationServices;
using Application.DataTransferObjects;
using Application.Repositories;
using Core.Application;
using Domain.Entities;

namespace Application.ApplicationServices
{
    public class AutomovilApplicationService : IAutomovilApplicationService
    {
        private readonly IAutomovilRepository _automovilRepository;

        public AutomovilApplicationService(IAutomovilRepository automovilRepository)
        {
            _automovilRepository = automovilRepository ?? throw new ArgumentNullException(nameof(automovilRepository));
        }

        public async Task<IEnumerable<AutomovilDto>> ObtenerTodosAsync()
        {
            var automoviles = await _automovilRepository.FindAllAsync();
            return automoviles.Select(a => CustomMapper.Instance.Map<AutomovilDto>(a));
        }

        public async Task<AutomovilDto?> ObtenerPorIdAsync(int id)
        {
            var automovil = await _automovilRepository.FindOneAsync(id);
            return automovil != null ? CustomMapper.Instance.Map<AutomovilDto>(automovil) : null;
        }

        public async Task<int> CrearAsync(CrearAutomovilDto dto)
        {
            // Validar que el número de motor sea único
            if (await _automovilRepository.ExisteNumeroMotorAsync(dto.NumeroMotor))
                throw new InvalidOperationException($"Ya existe un automóvil con el número de motor {dto.NumeroMotor}");

            // Validar que el número de chasis sea único
            if (await _automovilRepository.ExisteNumeroChasisAsync(dto.NumeroChasis))
                throw new InvalidOperationException($"Ya existe un automóvil con el número de chasis {dto.NumeroChasis}");

            var automovil = new Automovil(dto.Marca, dto.Modelo, dto.Color, dto.Fabricacion, dto.NumeroMotor, dto.NumeroChasis);
            var id = await _automovilRepository.AddAsync(automovil);

            return (int)id;
        }

        public async Task<bool> ActualizarAsync(int id, ActualizarAutomovilDto dto)
        {
            var automovil = await _automovilRepository.FindOneAsync(id);
            if (automovil == null)
                return false;

            // Validar que el número de motor sea único (excluyendo el automóvil actual)
            if (await _automovilRepository.ExisteNumeroMotorAsync(dto.NumeroMotor, id))
                throw new InvalidOperationException($"Ya existe otro automóvil con el número de motor {dto.NumeroMotor}");

            // Validar que el número de chasis sea único (excluyendo el automóvil actual)
            if (await _automovilRepository.ExisteNumeroChasisAsync(dto.NumeroChasis, id))
                throw new InvalidOperationException($"Ya existe otro automóvil con el número de chasis {dto.NumeroChasis}");

            // Crear nuevo automóvil con los datos actualizados
            var automovilActualizado = new Automovil(dto.Marca, dto.Modelo, dto.Color, dto.Fabricacion, dto.NumeroMotor, dto.NumeroChasis);

            // Preservar el ID original
            var idProperty = typeof(Automovil).BaseType?.GetProperty("Id");
            idProperty?.SetValue(automovilActualizado, id);

            _automovilRepository.Update(id, automovilActualizado);
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var automovil = await _automovilRepository.FindOneAsync(id);
            if (automovil == null)
                return false;

            _automovilRepository.Remove(id);
            return true;
        }

        public async Task<IEnumerable<AutomovilDto>> BuscarPorMarcaAsync(string marca)
        {
            var automoviles = await _automovilRepository.BuscarPorMarcaAsync(marca);
            return automoviles.Select(a => CustomMapper.Instance.Map<AutomovilDto>(a));
        }

        public async Task<IEnumerable<AutomovilDto>> BuscarPorAnioFabricacionAsync(int anioInicio, int anioFin)
        {
            var automoviles = await _automovilRepository.BuscarPorAnioFabricacionAsync(anioInicio, anioFin);
            return automoviles.Select(a => CustomMapper.Instance.Map<AutomovilDto>(a));
        }

        public async Task<IEnumerable<AutomovilDto>> BuscarPorColorAsync(string color)
        {
            var automoviles = await _automovilRepository.BuscarPorColorAsync(color);
            return automoviles.Select(a => CustomMapper.Instance.Map<AutomovilDto>(a));
        }

        public async Task<AutomovilDto?> BuscarPorNumeroChasisAsync(string numeroChasis)
        {
            var automovil = await _automovilRepository.BuscarPorNumeroChasisAsync(numeroChasis);
            return automovil != null ? CustomMapper.Instance.Map<AutomovilDto>(automovil) : null;
        }

        public async Task<bool> ValidarNumeroMotorUnicoAsync(string numeroMotor, int? excluirId = null)
        {
            return !await _automovilRepository.ExisteNumeroMotorAsync(numeroMotor, excluirId);
        }

        public async Task<bool> ValidarNumeroChasisUnicoAsync(string numeroChasis, int? excluirId = null)
        {
            return !await _automovilRepository.ExisteNumeroChasisAsync(numeroChasis, excluirId);
        }
    }
}