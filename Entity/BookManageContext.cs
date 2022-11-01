#nullable disable
using System.Security.Cryptography;
using System.Text;
using BookStoreManage.DTO;
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
            this.SeedAccounts(modelBuilder);
            this.SeedRoles(modelBuilder);
        }

        private void SeedAccounts(ModelBuilder builder)
        {
            List<CreateAccountDto> list = new List<CreateAccountDto>(){
                new CreateAccountDto{AccountEmail="tthanhtung92@gmail.com",Password="abc",RoleID=2},
                new CreateAccountDto{AccountEmail="tungttse140963@fpt.edu.vn",Password="abc",RoleID=2},
                new CreateAccountDto{AccountEmail="hoangnhse140184@fpt.edu.vn",Password="abc",RoleID=2},
                new CreateAccountDto{AccountEmail="admin",Password="admin",RoleID=1},
                new CreateAccountDto{AccountEmail="staff",Password="staff",RoleID=3},
            };
            int i = 0;
            foreach (CreateAccountDto dto in list)
            {
                Base64Encode(list[i].AccountEmail, out string strEncode);
                CreatePasswordHash(list[i].Password, out byte[] passwordHash, out byte[] passwordSalt);
                Account account = new Account()
                {
                    AccountID = i + 1,
                    AccountEmail = strEncode,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true,
                    RoleID = list[i].RoleID
                };
                builder.Entity<Account>().HasData(account);
                i++;
            }
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
