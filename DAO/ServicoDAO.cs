using Microsoft.EntityFrameworkCore;
using OrdemGo.Data;
using OrdemGo.Models;

namespace OrdemGo.DAO;

public class ServicoDAO
{
    private readonly OrdemGoContext _context;

    public ServicoDAO(OrdemGoContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Servico>> ListarAsync()
    {
        return await _context.Servicos
            .AsNoTracking()
            .OrderBy(servico => servico.Descricao)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Servico servico)
    {
        await _context.Servicos.AddAsync(servico);
        await _context.SaveChangesAsync();
    }

    public Task<Servico?> ObterPorIdAsync(int id)
    {
        return _context.Servicos.FirstOrDefaultAsync(servico => servico.Id == id);
    }

    public Task SalvarAlteracoesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Servico servico)
    {
        _context.Servicos.Remove(servico);
        await _context.SaveChangesAsync();
    }
}
