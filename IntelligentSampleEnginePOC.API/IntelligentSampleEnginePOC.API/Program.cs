using IntelligentSampleEnginePOC.API.Core.Services;
using IntelligentSampleEnginePOC.API.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddDbContext<ISEdbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("iseDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
