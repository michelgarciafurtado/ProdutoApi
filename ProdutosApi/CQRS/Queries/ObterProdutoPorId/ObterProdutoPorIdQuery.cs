using MediatR;

namespace ProdutosApi.CQRS.Queries.ObterProdutoPorId
{
    public record ObterProdutoPorIdQuery(int Id) :IRequest<ProdutoObtido>;
}
