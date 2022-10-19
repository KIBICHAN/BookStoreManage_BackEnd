#nullable disable
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManage.Entity
{
    [Index(nameof(Book.BookName), IsUnique = true)]
    public class Book
    {
        [Key]
        [Required]
        public int BookID { get; set; }
        [MaxLength(100)]
        public string StripeID { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Enter Book Name")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "Enter Price")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Enter Quantity")]
        public int Quantity { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime DateOfPublished { get; set; }
        public int FieldID { get; set; }
        public virtual Field Field { get; set; }
        public int PublisherID { get; set; }
        public virtual Publisher Publisher { get; set; }
        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
