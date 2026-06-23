using MassTransit;

namespace ProdutosApi.Publisher
{
    public class Worker:BackgroundService
    {
        readonly ISendEndpointProvider sendEndpointProvider;

        public Worker(ISendEndpointProvider sendEndpointProvider)
        {
            this.sendEndpointProvider = sendEndpointProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:produto_queue"));
            while (!stoppingToken.IsCancellationRequested)
            {
                await endpoint.Send(new ProdutoCriadoEvento("Produto Teste", "Descrição do Produto Teste", "1", 10.0m));
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
