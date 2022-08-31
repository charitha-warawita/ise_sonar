using IntelligentSampleEnginePOC.API;
using IntelligentSampleEnginePOC.API.Core.Cint;
using IntelligentSampleEnginePOC.API.Core.Data;
using IntelligentSampleEnginePOC.API.Core.Interfaces;
using IntelligentSampleEnginePOC.API.Core.Model;
using IntelligentSampleEnginePOC.API.Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();

builder.Services.AddTransient<IProjectReferenceService, ProjectReferenceService>();
builder.Services.AddTransient<IReferenceContext, ReferenceContext>();

builder.Services.AddTransient<IQualificationService, QualificationService>();
builder.Services.AddTransient<IQualificationContext, QualificationContext>();

builder.Services.AddTransient<IQuotaService, QuotaService>();
builder.Services.AddTransient<IQuotaContext, QuotaContext>();

builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IProjectContext, ProjectContext>();

builder.Services.AddTransient<ITargetAudienceService, TargetAudienceService>();
builder.Services.AddTransient<ITargetAudienceContext, TargetAudienceContext>();

builder.Services.AddTransient<ISpecTransform, CintCustomTransform>();
builder.Services.AddTransient<IProjectCintContext, ProjectCintContext>();

builder.Services.AddTransient<IProjectValidator, ProjectValidator>();

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("ConnectionStrings"));

var cintApiSettings = builder.Configuration.GetSection("CintApiSettings");
builder.Services.Configure<CintApiSettings>(cintApiSettings);

builder.Services.AddHttpClient<ICintSamplingService, CintSamplingService>(client =>
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

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => c.SwaggerDoc ("v1", 
    new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Intelligent Sample Engine API - Proof of Concept version" }));

builder.Services.AddLogging();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("MyPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();
