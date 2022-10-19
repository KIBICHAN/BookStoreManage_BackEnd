#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreManage.Entity
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public double TotalAmount { get; set; }
        [MaxLength(20)]
        public bool OrderStatus { get; set; }
        public DateTime DateOfOrder { get; set; }
        public int AccountID {get; set;}
        public virtual Account Account { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
