using System.ComponentModel.DataAnnotations;

namespace BookStoreManage.Entity
{
    public class Field
    {
        [Key]
        public int FieldID { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "Enter Field Name")]
        public string FieldName { get; set; }

        public string FieldDescription { get; set; }

        public virtual ICollection<Book>? Books { get; set; }

    }
}
