//contenitore die miei service

using EsSettimanale.Models;
using Microsoft.EntityFrameworkCore;

namespace EsSettimanale.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Spedizione> Spedizioni { get; set; }
        public DbSet<AggiornamentoSpedizione> AggiornamentiSpedizioni { get; set; }
        public DbSet<Utente> Utenti { get; set; }
    }
}
