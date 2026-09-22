using Microsoft.EntityFrameworkCore;
using OrdemGo.Data;
using OrdemGo.Models;

namespace OrdemGo.DAO;

public class OrdemServicoDAO
{
    private readonly OrdemGoContext _context;

    public OrdemServicoDAO(OrdemGoContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<OrdemServico>> ListarAsync()
    {
        return await _context.OrdensServico
            .AsNoTracking()
            .Include(ordem => ordem.Cliente)
            .OrderByDescending(ordem => ordem.DataAbertura)
            .ThenByDescending(ordem => ordem.Id)
            .ToListAsync();
    }

    public async Task<IReadOnlyDictionary<string, int>> ContarPorStatusAsync()
    {
        var contagens = await _context.OrdensServico
            .AsNoTracking()
            .GroupBy(ordem => ordem.Status)
            .Select(grupo => new
            {
                Status = grupo.Key,
                Quantidade = grupo.Count()
            })
            .ToListAsync();

        return contagens.ToDictionary(
            item => item.Status,
            item => item.Quantidade,
            StringComparer.OrdinalIgnoreCase);
    }

    public async Task AdicionarAsync(OrdemServico ordem)
    {
        await _context.OrdensServico.AddAsync(ordem);
        await _context.SaveChangesAsync();
    }
}
