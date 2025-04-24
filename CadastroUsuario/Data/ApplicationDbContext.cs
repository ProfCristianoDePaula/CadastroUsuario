using CadastroUsuario.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CadastroUsuario.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais para o modelo, se necessário
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Venda>().ToTable("Vendas");
            modelBuilder.Entity<Relatorio>().ToTable("Relatorios");

            // Cadastrando as Roles padrão do Sistema 
            Guid AdminGuid = Guid.NewGuid();
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = AdminGuid.ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Vendedor",
                    NormalizedName = "VENDEDOR"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Gerente",
                    NormalizedName = "GERENTE"
                }
            );

        }
    }
}
