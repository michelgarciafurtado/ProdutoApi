namespace ProdutosApi.CQRS.Commands.CriarProduto
{
    public record ProdutoCriado(
    int Id,
    string Nome,
    bool Sucesso,
    string Mensagem
        );
}
