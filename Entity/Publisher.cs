using System.ComponentModel.DataAnnotations;

namespace BookStoreManage.Entity
{
    public class Publisher
    {
        [Key]
        public int PublisherID { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Enter Publisher Name")]
        public string PublisherName { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "Enter Publisher Address")]
        public string FieldAddress { get; set; }

        public virtual ICollection<Book>? Books { get; set; }
    }
}
