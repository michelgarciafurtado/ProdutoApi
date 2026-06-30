using ProdutosApi.Models;

namespace ProdutosApi.CQRS.Commands.CriarProduto;

public record ProdutoCriado(
string Mensagem,
bool Sucesso,
Produto? Produto
    );
