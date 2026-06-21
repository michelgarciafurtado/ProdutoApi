namespace ProdutosApi.Models
{
    public class Produto : BaseEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Categoria categoria { get; set; }
        public decimal Preco { get; set; }
        public int CategoriaId { get; internal set; }

        public Produto() { }

        public Produto(string nome, string descricao, Categoria categoria, decimal preco)
        {
            Nome = nome;
            Descricao = descricao;
            this.categoria = categoria;
            Preco = preco;
        }
    }
}
