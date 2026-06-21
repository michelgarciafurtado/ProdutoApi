namespace ProdutosApi.CQRS.Commands.AtualizarProduto
{
    public record ProdutoAtualizado(int Id, string Nome, bool Sucesso, string Mensagem);
    
}
