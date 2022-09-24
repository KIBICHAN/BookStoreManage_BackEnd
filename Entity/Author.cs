#nullable disable
using System.ComponentModel.DataAnnotations;

namespace BookStoreManage.Entity
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Enter Author Name")]
        public string AuthorName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
