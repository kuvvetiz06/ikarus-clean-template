using IKARUSWEB.Infrastructure.Extensions;
using IKARUSWEB.Application.Commands.CreateTenant;
using IKARUSWEB.Application.Mapping;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;





var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Diğer servisler...
builder.Services.AddInfrastructure(builder.Configuration);

// 1)Add MediatR
builder.Services.AddMediatR(cfg =>
{
    // Handlers, pre/post processors, vb. bu assembly’den taransın
    cfg.RegisterServicesFromAssemblyContaining<CreateTenantCommandHandler>();
});
// 2)AutoMapper
builder.Services.AddAutoMapper(typeof(TenantProfile).Assembly);

// 3) MVC + FluentValidation
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();         // ← IServiceCollection uzantısı
builder.Services.AddFluentValidationClientsideAdapters();    // ← opsiyonel, client-side doğrulama

// 4) Validator’ları kaydet
builder.Services.AddValidatorsFromAssemblyContaining<CreateTenantCommandValidator>();

// 5) Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// IKARUSWEB.API/Program.cs içinde, Build()’den hemen önce
app.UseExceptionHandler(errApp =>
{
    errApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        var feature = context.Features.Get<IExceptionHandlerPathFeature>();
        var ex = feature?.Error;

        var result = System.Text.Json.JsonSerializer.Serialize(new
        {
            error = ex?.Message,
            path = feature?.Path
        });
        await context.Response.WriteAsync(result);
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

