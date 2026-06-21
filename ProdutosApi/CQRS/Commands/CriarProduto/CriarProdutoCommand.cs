using MediatR;

namespace ProdutosApi.CQRS.Commands.CriarProduto
{
    public record CriarProdutoCommand
        (
        string Nome,
        string Descricao,
        decimal Preco,
        int CategoriaId
        ):IRequest<ProdutoCriado>;
}
