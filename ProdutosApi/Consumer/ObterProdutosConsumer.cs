using Compartilhado.Eventos;
using MassTransit;
using MediatR;

namespace ProdutosApi.Consumer;

public class ObterProdutosConsumer : IConsumer<ObterProdutosRequest>
{
    private readonly ILogger<ObterProdutosConsumer> _logger;
    private readonly IMediator _mediator;

    public ObterProdutosConsumer(ILogger<ObterProdutosConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ObterProdutosRequest> context)
    {
        _logger.LogInformation("Consumindo requisição de obter produtos.");
        var produtos = await _mediator.Send(new ObterTodosProdutosQuery());
        await context.RespondAsync<ObterProdutosResponse>(new ObterProdutosResponse(produtos));
    }
}
