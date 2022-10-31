#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace BookStoreManage.Entity
{
    public class Account
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AccountID { get; set; }
        [MaxLength(100)]
        // [Required(ErrorMessage = "Enter Account Name")]
        public string Owner { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Enter Account Email")]
        public string AccountEmail { get; set; }
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
        [MaxLength(20)]
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
        public int RoleID {get; set;}
        public virtual Role Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


        //RefreshToken "If you gonna migration comment this!"
        public string RefreshToken { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
