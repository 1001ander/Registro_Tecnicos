using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicos.Services;

public class ClientesService
{
    private readonly IDbContextFactory<Contexto> DbFactory;

    public ClientesService(IDbContextFactory<Contexto> dbFactory)
    {
        DbFactory = dbFactory;
    }

    public async Task<bool> Guardar(Clientes clientes)
    {
        if (!await Existe(clientes.ClienteId))
        {
            return await Insertar(clientes);
        }
        else
        {
            return await Modificar(clientes);
        }
    }

    private async Task<bool> Existe(int clienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .AnyAsync(c => c.ClienteId == clienteId);
    }

    private async Task<bool> Insertar(Clientes clientes)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Clientes.Add(clientes);
        return await contexto
            .SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Clientes clientes)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(clientes);
        return await contexto
            .SaveChangesAsync() > 0;
    }

    public async Task<Clientes?> Buscar(int clienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
    }

    public async Task<bool> Eliminar(int clienteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var cliente = await contexto.Clientes
            .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
        if (cliente != null)
        {
            contexto.Clientes.Remove(cliente);
            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Clientes
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ExisteCliente(int clienteId, string nombres)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
             return await contexto.Clientes
            .AnyAsync(c => c.ClienteId != clienteId && 
            c.Nombres!
            .ToLower() == nombres
            .ToLower());
    }
}