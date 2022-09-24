#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreManage.Entity
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Enter Total Amount")]
        public double TotalAmount { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Enter Order Status")]
        public double OrderStatus { get; set; }

        [Required(ErrorMessage = "Enter Date of order")]
        public DateTime DateOfOrder { get; set; }

        [ForeignKey("AccountID")]
        public virtual Account? Account { get; set; }


        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
