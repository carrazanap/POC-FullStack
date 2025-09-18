using Application.ApplicationServices;
using Application.DataTransferObjects;
using Application.Repositories;
using Core.Application.Mapping;
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
            var automoviles = await _automovilRepository.GetAllAsync();
            return automoviles.Select(a => CustomMapper.Instance.Map<AutomovilDto>(a));
        }

        public async Task<AutomovilDto?> ObtenerPorIdAsync(int id)
        {
            var automovil = await _automovilRepository.GetByIdAsync(id);
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
            var automovilCreado = await _automovilRepository.AddAsync(automovil);
            
            return automovilCreado.Id;
        }

        public async Task<bool> ActualizarAsync(int id, ActualizarAutomovilDto dto)
        {
            var automovil = await _automovilRepository.GetByIdAsync(id);
            if (automovil == null)
                return false;

            // Validar que el número de motor sea único (excluyendo el automóvil actual)
            if (await _automovilRepository.ExisteNumeroMotorAsync(dto.NumeroMotor, id))
                throw new InvalidOperationException($"Ya existe otro automóvil con el número de motor {dto.NumeroMotor}");

            // Validar que el número de chasis sea único (excluyendo el automóvil actual)
            if (await _automovilRepository.ExisteNumeroChasisAsync(dto.NumeroChasis, id))
                throw new InvalidOperationException($"Ya existe otro automóvil con el número de chasis {dto.NumeroChasis}");

            // Actualizar la información básica
            automovil.ActualizarInformacion(dto.Marca, dto.Modelo, dto.Color, dto.Fabricacion);
            
            // Actualizar números si han cambiado
            if (automovil.NumeroMotor != dto.NumeroMotor)
                automovil.ActualizarNumeroMotor(dto.NumeroMotor);
                
            if (automovil.NumeroChasis != dto.NumeroChasis)
                automovil.ActualizarNumeroChasis(dto.NumeroChasis);

            await _automovilRepository.UpdateAsync(automovil);
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var automovil = await _automovilRepository.GetByIdAsync(id);
            if (automovil == null)
                return false;

            await _automovilRepository.DeleteAsync(id);
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
