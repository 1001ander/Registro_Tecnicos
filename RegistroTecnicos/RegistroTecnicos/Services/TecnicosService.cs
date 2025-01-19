
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TecnicosService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Guardar(Tecnicos tecnicos)
        {
            if (!await Existe(tecnicos.TecnicoId))
                return await Insertar(tecnicos);
            else
                return await Modificar(tecnicos);
        }

        private async Task<bool> Existe(int tecnicosId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .AnyAsync(t => t.TecnicoId == tecnicosId);
        }

        private async Task<bool> Insertar(Tecnicos tecnicos)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Tecnicos.Add(tecnicos);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Tecnicos tecnicos)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(tecnicos);
            return await contexto
                .SaveChangesAsync() > 0;
        }

        public async Task<Tecnicos?> Buscar(int tecnicosId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .FirstOrDefaultAsync(t => t.TecnicoId == tecnicosId);
        }

        public async Task<bool> Eliminar(int tecnicosId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .Where(t => t.TecnicoId == tecnicosId)
                .ExecuteDeleteAsync() > 0;

        }

        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<bool> ExisteTecnico(int tecnicosId, string nombres)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnicos
                .AnyAsync(t => t.TecnicoId != tecnicosId &&
                    t.Nombres.ToLower().Equals(nombres.ToLower()));

        }
    }
}