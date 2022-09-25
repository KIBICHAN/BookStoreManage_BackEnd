using BookStoreManage.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using Microsoft.AspNetCore.Identity.UI;
using static Microsoft.AspNetCore.Identity.IdentityBuilder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using BookStoreManage.IRepository;
using BookStoreManage.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
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
builder.Services.AddScoped<IFieldRepository, FieldRepository>();

// //Jwt
// var key = Encoding.UTF8.GetBytes(builder.Configuration["ApplicationSettings: JWT_Secret"].ToString());

// builder.Services.AddAuthentication(x =>{
//     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(x => {
//     x.RequireHttpsMetadata = false;
//     x.SaveToken = false;
//     x.TokenValidationParameters = new TokenValidationParameters{
//         ValidateIssuerSigningKey = true,
//         IssuerSigningKey = new SymmetricSecurityKey(key),
//         ValidateIssuer = false,
//         ValidateAudience = false,
//         ClockSkew = TimeSpan.Zero
//     };
// });

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
