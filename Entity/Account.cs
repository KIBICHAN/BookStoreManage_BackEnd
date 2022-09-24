#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace BookStoreManage.Entity
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [MaxLength(100)]
        // [Required(ErrorMessage = "Enter Account Name")]
        public string AccountName { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Enter User Name")]
        public string UserName { get; set; }

        [MaxLength(1024)]
        [Required]
        [Column(TypeName = "varbinary(1024)")]
        public byte[] PasswordHash { get; set; }

        [MaxLength(1024)]
        [Required]
        [Column(TypeName = "varbinary(1024)")]
        public byte[] PasswordSalt { get; set; }

        // [MaxLength(100)]
        // [Required(ErrorMessage = "Enter Password")]
        // public string Password { get; set; }

        [MaxLength(200)]
        // [Required(ErrorMessage = "Enter Account Email")]
        public string AccountEmail { get; set; }

        [MaxLength(12)]
        // [Required(ErrorMessage = "Enter Account Phone")]
        public string Phone { get; set; }

        [MaxLength(200)]
        // [Required(ErrorMessage = "Enter Account Address")]
        public string AccountAddress { get; set; }

        public string Image { get; set; }


        [MaxLength(100)]
        // [Required(ErrorMessage = "Enter Country")]
        public string Country { get; set; }

        public bool Status { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
