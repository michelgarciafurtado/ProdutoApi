namespace ProdutosApi.Models.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> CriarProdutoAsync(Produto produto);
        Task<Produto> ObterProdutoPorIdAsync(int id);
        Task<IEnumerable<Produto>> ObterTodosProdutosAsync();
        Task<Produto> AtualizarProdutoAsync(Produto produto);
        Task<bool> DeletarProdutoAsync(int id);
    }
}
