using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SgiMadsi;
using SgiMadsi.Dashboard.Services;
using SgiMadsi.Shared.Services;
using SgiMadsi.Shared.Configuration;
using Supabase;
using SgiMadsi.Shared.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<IProductoServices, ProductoService>();
builder.Services.AddScoped<IProveedoresServices, ProveedoresService>();
builder.Services.AddScoped<IDetalleVentaServices, DetalleVentaService>();
builder.Services.AddScoped<IVentaServices, VentaService>();

//conexion a supabase

var supabaseOptions = builder.Configuration
    .GetSection("Supabase")
    .Get<SupabaseConfig>();

var client = new Client(
    supabaseOptions!.Url,
    supabaseOptions.Key
);

await client.InitializeAsync();

builder.Services.AddSingleton(client);



//fin conexion a supabase

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<ProductoService>();

await builder.Build().RunAsync();
