#nullable disable
using System.Security.Cryptography;
using System.Text;
using BookStoreManage.DTO;
using BookStoreManage.IRepository;
using BookStoreManage.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManage.Entity
{
    public class BookManageContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Field> Fields { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public BookManageContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Hàm này để ép dữ liệu mặc định
            this.SeedAccountsCus(modelBuilder);
            this.SeedAccountsCus1(modelBuilder);
            this.SeedAccountsCus2(modelBuilder);
            this.SeedAccountAdmin(modelBuilder);
            this.SeedAccountStaff(modelBuilder);
            this.SeedRoles(modelBuilder);
        }

        private void SeedAccountsCus(ModelBuilder builder)
        {
            CreatePasswordHash("abc", out byte[] passwordHash, out byte[] passwordSalt);
            Base64Encode("tthanhtung92@gmail.com", out string strEncode);
            Account account = new Account()
            {
                AccountID = 1,
                AccountEmail = strEncode,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleID = 2
            };
            builder.Entity<Account>().HasData(account);
        }
        private void SeedAccountsCus1(ModelBuilder builder)
        {
            CreatePasswordHash("abc", out byte[] passwordHash, out byte[] passwordSalt);
            Base64Encode("tungttse140963@fpt.edu.vn", out string strEncode);
            Account account = new Account()
            {
                AccountID = 2,
                AccountEmail = strEncode,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleID = 2
            };
            builder.Entity<Account>().HasData(account);
        }
        private void SeedAccountsCus2(ModelBuilder builder)
        {
            CreatePasswordHash("abc", out byte[] passwordHash, out byte[] passwordSalt);
            Base64Encode("hoangnhse140184@fpt.edu.vn", out string strEncode);
            Account account = new Account()
            {
                AccountID = 3,
                AccountEmail = strEncode,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleID = 2
            };
            builder.Entity<Account>().HasData(account);
        }

       
        private void SeedAccountAdmin(ModelBuilder builder)
        {
            CreatePasswordHash("123", out byte[] passwordHash, out byte[] passwordSalt);
            Base64Encode("admin", out string strEncode);
            Account account = new Account()
            {
                AccountID = 4,
                AccountEmail = strEncode,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleID = 1
            };
            builder.Entity<Account>().HasData(account);
        }
        private void SeedAccountStaff(ModelBuilder builder)
        {
            CreatePasswordHash("123", out byte[] passwordHash, out byte[] passwordSalt);
            Base64Encode("staff", out string strEncode);
            Account account = new Account()
            {
                AccountID = 5,
                AccountEmail = strEncode,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleID = 3
            };
            builder.Entity<Account>().HasData(account);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(new Role()
            {
                RoleID = 1,
                RoleName = "Admin"
            });
            builder.Entity<Role>().HasData(new Role()
            {
                RoleID = 2,
                RoleName = "Customer"
            });
            builder.Entity<Role>().HasData(new Role()
            {
                RoleID = 3,
                RoleName = "Staff"
            });
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public void Base64Encode(string textStr, out string strEncode)
        {
            var textbytes = Encoding.UTF8.GetBytes(textStr);
            strEncode = Convert.ToBase64String(textbytes);
        }
    }
}
