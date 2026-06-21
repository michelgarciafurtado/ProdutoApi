using MediatR;

namespace ProdutosApi.CQRS.Commands.AtualizarProduto
{
    public record AtualizarProdutoCommand(int Id, string Nome, decimal Preco, int CategoriaId):IRequest<ProdutoAtualizado>;
    
}
