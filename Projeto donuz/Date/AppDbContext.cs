using Microsoft.EntityFrameworkCore;
using Projeto_donuz.Model;

namespace Projeto_donuz.AppDbContex
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Cliente> Cadastros { get; set; }

        public DbSet<Transacao> Transacaos { get; set; }     
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=shared");

    }
}
