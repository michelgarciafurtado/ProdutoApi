using MediatR;

namespace ProdutosApi.CQRS.Queries.ObterProdutos;

public record ObterTodosProdutosQuery():IRequest<IEnumerable<ProdutoObtido>>;
