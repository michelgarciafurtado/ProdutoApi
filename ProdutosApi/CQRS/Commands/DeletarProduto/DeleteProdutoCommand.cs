using MediatR;

namespace ProdutosApi.CQRS.Commands.DeletarProduto
{
    public record DeleteProdutoCommand(int id) :IRequest<bool>;
    
}
