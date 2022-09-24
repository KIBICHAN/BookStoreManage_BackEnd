#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreManage.Entity
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        [Required(ErrorMessage = "Enter Price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Enter Total Quantity")]
        public double Quantity { get; set; }

        [Required(ErrorMessage = "Enter Total Price")]
        public double TotalPrice { get; set; }


        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
    }
}
