using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutosApi.CQRS.Commands.AtualizarProduto;
using ProdutosApi.CQRS.Commands.CriarProduto;
using ProdutosApi.CQRS.Queries.ObterProdutoPorId;
using ProdutosApi.CQRS.Queries.ObterProdutos;

namespace ProdutosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ILogger<ProdutosController> _logger;
        private readonly IMediator _mediator;

        public ProdutosController(ILogger<ProdutosController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CriarProdutoAsync([FromBody] CriarProdutoCommand command)
        {
            var resultado = await _mediator.Send(command);
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado.Mensagem);
            };
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
    }
}
