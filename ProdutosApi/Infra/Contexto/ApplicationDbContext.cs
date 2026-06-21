using Microsoft.EntityFrameworkCore;
using ProdutosApi.Models;

namespace ProdutosApi.Infra.Contexto
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; } // Nova tabela
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais do modelo podem ser feitas aqui
            modelBuilder.Entity<Produto>(
                builder =>
                {
                    builder.HasKey(p => p.Id); // Configura a chave primária para a entidade Produto
                    builder.Property(p => p.Id).UseIdentityColumn(); // Auto -incremento para a coluna Id, vem do pacote SQLServer
                    builder.Property(p => p.Nome).IsRequired().HasMaxLength(100);
                    builder.Property(p => p.Descricao).HasMaxLength(255);
                    builder.Property(p => p.Preco).HasColumnType("decimal(18,2)");
                    builder.HasOne(p => p.categoria) 
                        .WithMany() 
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity<Categoria>(c =>
            {
                c.HasKey(p => p.Id);
                c.Property(p => p.Id).UseIdentityColumn();
                c.Property(p => p.nome).IsRequired().HasMaxLength(100);
            });
               
        }
    }
}
