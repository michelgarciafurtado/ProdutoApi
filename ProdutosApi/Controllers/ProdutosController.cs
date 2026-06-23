using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutosApi.CQRS.Commands.AtualizarProduto;
using ProdutosApi.CQRS.Commands.CriarProduto;
using ProdutosApi.CQRS.Commands.DeletarProduto;
using ProdutosApi.CQRS.Queries.ObterProdutoPorId;
using ProdutosApi.CQRS.Queries.ObterProdutos;
using ProdutosApi.Publisher;

namespace ProdutosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ILogger<ProdutosController> _logger;
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProdutosController(ILogger<ProdutosController> logger, IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }
        [HttpPost]
        public async Task<IActionResult> CriarProdutoAsync([FromBody] CriarProdutoCommand command)
        {
            var resultado = await _mediator.Send(command);
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado.Mensagem);
            };

            // Publicar evento de produto criado
            await _publishEndpoint.Publish(new ProdutoCriadoEvento(command.Nome, command.Descricao, Convert.ToString(command.CategoriaId), command.Preco));

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosProdutosAsync()
        {
            return Ok(await _mediator.Send(new ObterTodosProdutosQuery()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterProdutoPorIdAsync(int id)
        {
            return Ok(await _mediator.Send(new ObterProdutoPorIdQuery(id)));
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarProdutoAsync([FromBody]AtualizarProdutoCommand produto)
        {
            var resultado = await _mediator.Send(produto);
            if (resultado.Sucesso)
                return Ok(resultado);
            return BadRequest(resultado);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirProdutoAsync(int id)
        {
            var resultado = await _mediator.Send(new DeleteProdutoCommand(id));
            if (resultado)
                return Ok(resultado);
            return BadRequest(resultado);
        }
    }
}
