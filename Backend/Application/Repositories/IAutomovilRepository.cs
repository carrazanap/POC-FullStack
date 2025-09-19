using Core.Application.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IAutomovilRepository : IRepository<Automovil>
    {
        Task<bool> ExisteNumeroMotorAsync(string numeroMotor, int? excluirId = null);
        Task<bool> ExisteNumeroChasisAsync(string numeroChasis, int? excluirId = null);
        Task<IEnumerable<Automovil>> BuscarPorMarcaAsync(string marca);
        Task<IEnumerable<Automovil>> BuscarPorAnioFabricacionAsync(int anioInicio, int anioFin);
        Task<IEnumerable<Automovil>> BuscarPorColorAsync(string color);
    }
}
