#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreManage.Entity
{
    public class Book
    {
        [Key]
        [Required]
        public int BookID { get; set; }


        [MaxLength(200)]
        [Required(ErrorMessage = "Enter Book Name")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Enter Price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Enter Quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Enter Image")]
        public string Image { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Enter Date of published")]
        public DateTime DateOfPublished { get; set; }

        public int FieldID {get; set;}
        public virtual Field Field { get; set; }

        public int PublisherID {get; set;}
        public virtual Publisher Publisher { get; set; }

        public int AuthorID {get; set;}
        public virtual Author Author { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
