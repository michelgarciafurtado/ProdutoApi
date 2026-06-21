using MediatR;
using ProdutosApi.Models.Interfaces;

namespace ProdutosApi.CQRS.Queries.ObterProdutoPorId
{
    public class ObterProdutoPorIdQueryHandler : IRequestHandler<ObterProdutoPorIdQuery, ProdutoObtido>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ObterProdutoPorIdQueryHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<ProdutoObtido> Handle(ObterProdutoPorIdQuery request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterProdutoPorIdAsync(request.Id);
            return new ProdutoObtido(
                produto.Id,
                produto.Nome, 
                produto.Descricao,
                produto.Preco,
                produto.categoria.Id,
                produto.categoria.nome);
        }
    }}
