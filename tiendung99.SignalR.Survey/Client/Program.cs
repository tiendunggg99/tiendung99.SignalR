using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using tiendung99.SignalR.Survey.Client;
using tiendung99.SignalR.Survey.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<SurveyHttpClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();
