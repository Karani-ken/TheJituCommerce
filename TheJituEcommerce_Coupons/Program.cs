using Microsoft.EntityFrameworkCore;
using TheJituEcommerce_Coupons.Data;
using TheJituEcommerce_Coupons.Extensions;
using TheJituEcommerce_Coupons.Services;
using TheJituEcommerce_Coupons.Services.IService;

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

//add service
builder.Services.AddScoped<ICouponInterface, CouponService>();


//auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//add custom services
builder.AddSwaggenGenExtension();
builder.AddAppAuthentication();
builder.Services.AddCors(options => options.AddPolicy("policy1", build =>
{
	build.AllowAnyOrigin();
	build.AllowAnyHeader();
	build.AllowAnyMethod();
}));

var app = builder.Build();
Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:Key").Get<string>();
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

app.Run();
