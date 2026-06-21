using MediatR;
using ProdutosApi.Models.Interfaces;

namespace ProdutosApi.CQRS.Commands.AtualizarProduto
{
    public class AtualizarProdutoCommandHandler : IRequestHandler<AtualizarProdutoCommand, ProdutoAtualizado>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ILogger<AtualizarProdutoCommandHandler> _logger;

        public AtualizarProdutoCommandHandler(IProdutoRepository produtoRepository, ILogger<AtualizarProdutoCommandHandler> logger)
        {
            _produtoRepository = produtoRepository;
            _logger = logger;
        }
    
        public async Task<ProdutoAtualizado> Handle(AtualizarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterProdutoPorIdAsync(request.Id);
            if (produto == null)
            {
                _logger.LogWarning("Produto não encontrado: {Id}", request.Id);
                return new ProdutoAtualizado(request.Id, string.Empty, false, "Produto não encontrado");
            }

            produto.Nome = request.Nome;
            produto.Preco = request.Preco;
            produto.CategoriaId = request.CategoriaId;

            var produtoAtualizado = await _produtoRepository.AtualizarProdutoAsync(produto);
            return new ProdutoAtualizado(produtoAtualizado.Id, produtoAtualizado.Nome, true, "Produto atualizado com sucesso");
        }
    }
}
