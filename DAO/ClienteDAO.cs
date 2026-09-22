using Microsoft.EntityFrameworkCore;
using OrdemGo.Data;
using OrdemGo.Models;

namespace OrdemGo.DAO;

public class ClienteDAO
{
    private readonly OrdemGoContext _context;

    public ClienteDAO(OrdemGoContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Cliente>> ListarAsync()
    {
        return await _context.Clientes
            .AsNoTracking()
            .OrderBy(cliente => cliente.Nome)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public Task<Cliente?> ObterPorIdAsync(int id)
    {
        return _context.Clientes.FirstOrDefaultAsync(cliente => cliente.Id == id);
    }

    public Task SalvarAlteracoesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
    }
}
