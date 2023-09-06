using Microsoft.EntityFrameworkCore;
using TheJitu_Ecommerce_Cart.Data;
using TheJitu_Ecommerce_Cart.Extensions;
using TheJitu_Ecommerce_Cart.Services;
using TheJitu_Ecommerce_Cart.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//register service
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductInterface, ProductService>();
builder.Services.AddScoped<ICouponService, CouponService>();
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//register base urls for services communications
builder.Services.AddHttpClient("Product", c=>c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:ProductApi"]));
builder.Services.AddHttpClient("Coupon", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:Product"]));

builder.AddSwaggenGenExtension();
builder.AddAppAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//migration
app.UseMigration();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
