using Microsoft.EntityFrameworkCore;
using ProdutosApi.Infra.Contexto;
using ProdutosApi.Models;
using ProdutosApi.Models.Interfaces;

namespace ProdutosApi.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        async Task<Produto> IProdutoRepository.AtualizarProdutoAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

         async Task<Produto> IProdutoRepository.CriarProdutoAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return _context.Produtos
                    .Include(p => p.categoria)
                    .FirstOrDefault(p => p.Id == produto.Id)!;
        }

        async Task<bool> IProdutoRepository.DeletarProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<Produto> IProdutoRepository.ObterProdutoPorIdAsync(int id)
        {
            return await _context.Produtos
                .Include(p => p.categoria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        async Task<IEnumerable<Produto>> IProdutoRepository.ObterTodosProdutosAsync()
        {
            return await _context.Produtos
                .Include(p => p.categoria)
                .ToListAsync();
        }
    }
}
