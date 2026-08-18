using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Piratenet;
using Piratenet.Services;

// Crea el host de Blazor WebAssembly con la configuracion del entorno actual.
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Registra un HttpClient con como base la URL desde la que se sirve la aplicacion.
// Esto permite que los recursos relativos funcionen tanto en local como en GitHub Pages.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<LanguageState>();

// Construye el host y mantiene la aplicacion ejecutandose.
await builder.Build().RunAsync();
