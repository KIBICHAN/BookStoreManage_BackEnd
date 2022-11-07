using BookStoreManage.Entity;
using Microsoft.EntityFrameworkCore;
using BookStoreManage.IRepository;
using BookStoreManage.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(options => options.Listen(System.Net.IPAddress.Parse("192.168.137.71"), 7132));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header uisng the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("D:\\Swp391\\BookStoreManage\\firebaseconfig.json"),
    ProjectId = "book-store-management-abf1d",
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:TokenSecret").Value)),
        ValidateIssuer = false,
        ValidateAudience = false,
        // ValidIssuer = "mytest.com",
        // ValidAudience = "mytest.com",
    };
});

var connectionString = builder.Configuration.GetConnectionString("BookManageContextConection") ??
    throw new InvalidOperationException("Connection string 'BookManageContextConection' not found.");

builder.Services.AddDbContext<BookManageContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    // build.WithOrigins("https://localhost:7091");
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IFieldRepository, FieldRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCors");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
