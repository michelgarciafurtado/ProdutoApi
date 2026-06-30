using MediatR;
using ProdutosApi.CQRS.Queries;

namespace Compartilhado.Eventos;

public record ObterTodosProdutosQuery():IRequest<IEnumerable<ProdutoObtido>>;
