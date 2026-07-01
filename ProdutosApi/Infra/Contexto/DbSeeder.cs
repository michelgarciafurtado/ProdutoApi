namespace ProdutosApi.Infra.Contexto
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Categorias.Any())
            {
                context.Categorias.AddRange(
                    new Models.Categoria { nome = "Lanches" },
                    new Models.Categoria { nome = "Porções" },
                    new Models.Categoria { nome = "Bebidas" }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Produtos.Any())
            {
                context.Produtos.AddRange(
                    new Models.Produto { Nome = "X-Salada", Descricao = "Hambúrguer com queijo e alface", Preco = 15.90m, CategoriaId = 1 },
                    new Models.Produto { Nome = "X-Burger", Descricao = "Hambúrguer com bacon e queijo", Preco = 17.90m, CategoriaId = 1 },
                    new Models.Produto { Nome = "Batata Frita", Descricao = "Batata frita crocante", Preco = 12.90m, CategoriaId = 2 }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}

