namespace Aversion.Data.Entities
{
  public class OrderItem
  {
    public int Id { get; set; }
    public decimal Price { get; set; }
    public Product Product { get; set; }
    public double Quantity { get; set; }
  }
}