using Application.DataTransferObjects;

namespace Application.ApplicationServices
{
    public interface IAutomovilApplicationService
    {
        Task<IEnumerable<AutomovilDto>> ObtenerTodosAsync();
        Task<AutomovilDto?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(CrearAutomovilDto dto);
        Task<bool> ActualizarAsync(int id, ActualizarAutomovilDto dto);
        Task<bool> EliminarAsync(int id);
        Task<IEnumerable<AutomovilDto>> BuscarPorMarcaAsync(string marca);
        Task<IEnumerable<AutomovilDto>> BuscarPorAnioFabricacionAsync(int anioInicio, int anioFin);
        Task<IEnumerable<AutomovilDto>> BuscarPorColorAsync(string color);
        Task<AutomovilDto?> BuscarPorNumeroChasisAsync(string numeroChasis);
        Task<bool> ValidarNumeroMotorUnicoAsync(string numeroMotor, int? excluirId = null);
        Task<bool> ValidarNumeroChasisUnicoAsync(string numeroChasis, int? excluirId = null);
    }
}