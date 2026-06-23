using MediatR;
using ProdutosApi.Models.Interfaces;

namespace ProdutosApi.CQRS.Commands.DeletarProduto;

public class DeleteProdutoCommandHandler : IRequestHandler<DeleteProdutoCommand, bool>
{
    private readonly IProdutoRepository _produtoRepository;

    public DeleteProdutoCommandHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<bool> Handle(DeleteProdutoCommand request, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.ObterProdutoPorIdAsync(request.id);
        if (produto == null)
            return false;

        return await _produtoRepository.DeletarProdutoAsync(produto.Id);
    }
}
