namespace OrderAPI;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }

    public DateOnly Date { get; set; }

    public  int Quantity { get; set; }


}
