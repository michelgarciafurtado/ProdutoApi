namespace ProdutosApi.CQRS.Queries;

public record ProdutoObtido(
    int Id,
    string Nome,
    string Descricao,
    decimal Preco,
    int CategoriaId,
    string CategoriaNome
);
