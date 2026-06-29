using ProdutosApi.CQRS.Queries;

namespace ProdutosApi.Consumer
{
    public record ObterProdutosResponse(IEnumerable<ProdutoObtido> Produtos);
   
}
