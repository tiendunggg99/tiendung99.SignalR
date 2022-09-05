using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using tiendung99.SignalR.Survey.Client;
using tiendung99.SignalR.Survey.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseAddress = new Uri(builder.HostEnvironment.BaseAddress);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = baseAddress });
// Configure a typed HttpClient with all the survey API endpoints
// so the razor pages/components dont need to use the raw HttpClient
builder.Services.AddHttpClient<SurveyHttpClient>(client => client.BaseAddress = baseAddress);

// Register a preconfigure SignalR hub connection.
// Note the connection isnt yet started, this will be done as part of the App.razor component
// to avoid blocking the application startup in case the connection cannot be established
builder.Services.AddSingleton<HubConnection>(sp => {
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HubConnectionBuilder()
      .WithUrl(navigationManager.ToAbsoluteUri("/surveyhub"))
      .WithAutomaticReconnect()
      .Build();
});

await builder.Build().RunAsync();
