using Eficientiza_s3.Data.AppData;
using Eficientiza_s3.Data.Repositories;
using Eficientiza_s3.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options => {
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle"));
});

// Add services to the container.
builder.Services.AddTransient<IEstacaoRepository, EstacaoRepository>();
builder.Services.AddTransient<IMotoRepository, MotoRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
    c.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

builder.Services.AddRateLimiter(options =>
{
    // Definindo a poltica de limitao de taxa
    options.AddFixedWindowLimiter("rateLimitePolicy", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromSeconds(10);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });


    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;


    options.OnRejected = async (context, _) =>
    {
        context.HttpContext.Response.Headers.Append("X-RateLimit-Limit", "5");
        context.HttpContext.Response.Headers.Append("X-RateLimit-Remaining", "0");
        context.HttpContext.Response.Headers.Append("X-RateLimit-Reset", DateTime.UtcNow.AddSeconds(10).ToString("r"));
        await Task.CompletedTask;
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseRateLimiter();

app.Run();
