namespace Compartilhado.Eventos;

public record ProdutoCriadoEvento
    (int id, string nome, string descricao, string categoria, decimal preco);

