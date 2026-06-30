using ProdutosApi.CQRS.Queries;

namespace Compartilhado.Eventos
{
    public record ObterProdutosResponse(IEnumerable<ProdutoObtido> Produtos);
   
}
