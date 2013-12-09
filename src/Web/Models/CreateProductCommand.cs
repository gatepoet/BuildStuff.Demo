namespace Web.Models
{
    public class CreateProductCommand
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}