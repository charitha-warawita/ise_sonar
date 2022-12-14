using IntelligentSampleEnginePOC.API;
using IntelligentSampleEnginePOC.API.Core.Services;
using IntelligentSampleEnginePOC.API.DB;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddDbContext<ISEdbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("iseDb")));

var cintApiSettings = builder.Configuration.GetSection("CintApiSettings");
builder.Services.Configure<CintApiSettings>(cintApiSettings);

builder.Services.AddHttpClient<ICintService, CintService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("CintApiSettings:Url"));
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Add("x-api-key", builder.Configuration.GetValue<string>("CintApiSettings:x-api-key"));
    client.DefaultRequestHeaders.Add("Authorization", builder.Configuration.GetValue<string>("CintApiSettings:Authorization"));
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
    return handler;
})
.SetHandlerLifetime(TimeSpan.FromMinutes(5));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
