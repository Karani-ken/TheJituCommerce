using Microsoft.EntityFrameworkCore;
using TheJituEcommerce_OrderService.Data;
using TheJituEcommerce_OrderService.Extensions;
using TheJituEcommerce_OrderService.Services;
using TheJituEcommerce_OrderService.Services.IService;

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
builder.Services.AddScoped<IOrderInterface, OrderService>();
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.AddAppAuthentication();
builder.AddSwaggenGenExtension();

builder.Services.AddCors(options => options.AddPolicy("policy1", build =>
{
	build.AllowAnyOrigin();
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
//register for stripe
Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:Key").Get<string>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("policy1");
app.MapControllers();
app.UseMigration();
app.Run();
