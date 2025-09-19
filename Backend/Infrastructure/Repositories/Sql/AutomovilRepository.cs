using Application.Repositories;
using Core.Infraestructure.Repositories.Sql;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Sql
{
    internal class AutomovilRepository : BaseRepository<Automovil>, IAutomovilRepository
    {
        public AutomovilRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<bool> ExisteNumeroMotorAsync(string numeroMotor, int? excluirId = null)
        {
            return await Context.Set<Automovil>()
                .AnyAsync(a => a.NumeroMotor == numeroMotor && (excluirId == null || a.Id != excluirId));
        }

        public async Task<bool> ExisteNumeroChasisAsync(string numeroChasis, int? excluirId = null)
        {
            return await Context.Set<Automovil>()
                .AnyAsync(a => a.NumeroChasis == numeroChasis && (excluirId == null || a.Id != excluirId));
        }

        public async Task<IEnumerable<Automovil>> BuscarPorMarcaAsync(string marca)
        {
            return await Context.Set<Automovil>()
                .Where(a => a.Marca.Contains(marca))
                .ToListAsync();
        }

        public async Task<IEnumerable<Automovil>> BuscarPorAnioFabricacionAsync(int anioInicio, int anioFin)
        {
            return await Context.Set<Automovil>()
                .Where(a => a.Fabricacion >= anioInicio && a.Fabricacion <= anioFin)
                .ToListAsync();
        }

        public async Task<IEnumerable<Automovil>> BuscarPorColorAsync(string color)
        {
            return await Context.Set<Automovil>()
                .Where(a => a.Color.Contains(color))
                .ToListAsync();
        }

        public async Task<Automovil?> BuscarPorNumeroChasisAsync(string numeroChasis)
        {
            return await Context.Set<Automovil>()
                .FirstOrDefaultAsync(a => a.NumeroChasis == numeroChasis);
        }
    }
}