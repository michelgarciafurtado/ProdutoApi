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
        }
    }
}
