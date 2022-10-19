#nullable disable
namespace BookStoreManage.DTO
{
    public class BookDTO
    {
        public string stripeId {get; set;}
        public string bookName { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public DateTime DateOfPublished { get; set; }
        public int fieldID { get; set; }
        public int publisherID { get; set; }
        public int authorID { get; set; }
    }
}
