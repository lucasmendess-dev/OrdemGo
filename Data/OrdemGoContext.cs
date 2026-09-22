using Microsoft.EntityFrameworkCore;
using OrdemGo.Models;

namespace OrdemGo.Data;

public class OrdemGoContext : DbContext
{
    public OrdemGoContext(DbContextOptions<OrdemGoContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
}
