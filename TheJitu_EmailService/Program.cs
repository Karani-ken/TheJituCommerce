using Microsoft.EntityFrameworkCore;
using TheJitu_EmailService.Data;
using TheJitu_EmailService.Extensions;
using TheJitu_EmailService.Messaging;

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
//services
builder.Services.AddSingleton<IAzureMessageBusConsumer, AzureMessagingBusConsumer>();
//var dbContextBuilder = new DbContextOptionsBuilder<AppDbContext>();
//dbContextBuilder.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
//builder.Services.AddSingleton(new EmailService(dbContextBuilder.Options));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.useAzure();
app.UseMigration();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
