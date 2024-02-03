using Blazored.LocalStorage;
using Client_Side;
using Client_Side.Services.Authentication;
using Client_Side.Services.AuthProvider;
using Client_Side.Services.Cart;
using Client_Side.Services.Orders;
using Client_Side.Services.Product;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//register localstorage
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IProductInterface, ProductService>();
builder.Services.AddScoped<IAuthInterface, AuthService>();
builder.Services.AddScoped<ICartInterface, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

//configuring authProvider
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProviderService>();
await builder.Build().RunAsync();
