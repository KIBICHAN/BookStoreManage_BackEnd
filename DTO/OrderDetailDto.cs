namespace BookStoreManage.DTO;

public class OrderDetailDto
{
    public int OrderID { get; set; }
    public int BookID { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
}