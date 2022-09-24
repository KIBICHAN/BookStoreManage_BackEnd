using BookStoreManage.Entity;
using Microsoft.EntityFrameworkCore;
using BookStoreManage.IRepository;
using BookStoreManage.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => options.Listen(System.Net.IPAddress.Parse("192.168.43.108"), 7132));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("BookManageContextConection") ??
    throw new InvalidOperationException("Connection string 'BookManageContextConection' not found.");


builder.Services.AddDbContext<BookManageContext>(options => {
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IAuthRepository, AuthRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
