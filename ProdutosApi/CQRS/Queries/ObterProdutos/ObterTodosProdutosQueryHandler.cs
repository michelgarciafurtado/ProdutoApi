using MediatR;
using ProdutosApi.Models.Interfaces;

namespace ProdutosApi.CQRS.Queries.ObterProdutos
{
    public class ObterTodosProdutosQueryHandler : IRequestHandler<ObterTodosProdutosQuery, IEnumerable<ProdutoObtido>>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ObterTodosProdutosQueryHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<ProdutoObtido>> Handle(ObterTodosProdutosQuery request, CancellationToken cancellationToken)
        {
            var produtos = await _produtoRepository.ObterTodosProdutosAsync();
            return produtos.Select(p => new ProdutoObtido(
                p.Id,
                p.Nome,
                p.Descricao,
                p.Preco,
                p.categoria.Id,
                p.categoria.nome
            ));
        }
    }
}
