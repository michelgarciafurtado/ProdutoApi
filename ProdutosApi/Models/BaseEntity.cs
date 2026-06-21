namespace ProdutosApi.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; } = DateTime.Now;
    }
}
