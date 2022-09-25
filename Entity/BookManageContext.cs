#nullable disable
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
            this.SeedRoles(modelBuilder);
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
    }
}
