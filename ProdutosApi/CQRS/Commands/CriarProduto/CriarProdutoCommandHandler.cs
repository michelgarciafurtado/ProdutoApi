using MediatR;
using ProdutosApi.Models.Interfaces;

namespace ProdutosApi.CQRS.Commands.CriarProduto
{
    public class CriarProdutoCommandHandler : IRequestHandler<CriarProdutoCommand, ProdutoCriado>
    {
        private readonly ILogger<CriarProdutoCommandHandler> _logger;
        private readonly IProdutoRepository _produtoRepository;
        public CriarProdutoCommandHandler(ILogger<CriarProdutoCommandHandler> logger, IProdutoRepository produtoRepository)
        {
            _logger = logger;
            _produtoRepository = produtoRepository;
        }
        public async Task<ProdutoCriado> Handle(CriarProdutoCommand request, CancellationToken cancellationToken)
        {
           var produto = await _produtoRepository.CriarProdutoAsync(
                new Models.Produto()
                {
                    Nome = request.Nome,
                    Descricao = request.Descricao,
                    Preco = request.Preco,
                    CategoriaId = request.CategoriaId
                });
            if (produto != null) 
            {
                _logger.LogInformation("Produto criado com sucesso: {ProdutoId}, em {Data}", produto.Id, DateTime.UtcNow);
                return new ProdutoCriado("Produto criado com sucesso.", true, produto);

            }
            return new ProdutoCriado("Erro ao criar produto.", false, null);
        }
    }
}
