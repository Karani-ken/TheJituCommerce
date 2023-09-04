using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheJituEcommerce_Auth.Data;
using TheJituEcommerce_Auth.Extensions;
using TheJituEcommerce_Auth.Models;
using TheJituEcommerce_Auth.Services;
using TheJituEcommerce_Auth.Services.IService;

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
//register services
builder.Services.AddScoped<IUserInterface, UserService>();
//register identity Framework
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMigration();
app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
