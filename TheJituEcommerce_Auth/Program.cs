using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceBus;
using TheJituEcommerce_Auth.Data;
using TheJituEcommerce_Auth.Extensions;
using TheJituEcommerce_Auth.Models;
using TheJituEcommerce_Auth.Services;
using TheJituEcommerce_Auth.Services.IService;
using TheJituEcommerce_Auth.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//connect db
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//register identity Framework
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
//register services
builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<IJwtInterface, JwtService>();
builder.Services.AddScoped<IMessageBus, MessageBus>();

//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//configure JWtOptions 
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddCors(options => options.AddPolicy("policy1", build =>
{
    build.WithOrigins("https://localhost:7269");
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMigration();
app.UseHttpsRedirection();
app.UseCors("policy1");
app.UseAuthorization();


app.MapControllers();
app.UseCors("policy1");
app.Run();
