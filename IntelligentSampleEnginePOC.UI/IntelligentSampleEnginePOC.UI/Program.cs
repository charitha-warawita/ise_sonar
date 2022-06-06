using IntelligentSampleEnginePOC.UI.Configurations;
using IntelligentSampleEnginePOC.UI.Core;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var iseApiSettings = builder.Configuration.GetSection("ISEApiSettings");
builder.Services.Configure<ISEApiSettings>(iseApiSettings);


builder.Services.AddHttpClient<IProjectVMService, ProjectVMService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ISEApiSettings:Url"));
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler =  new HttpClientHandler();
    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
    return handler;
})
.SetHandlerLifetime(TimeSpan.FromMinutes(5));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Project}/{action=Index}/{id?}");

app.Run();
