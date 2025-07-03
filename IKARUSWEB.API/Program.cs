using IKARUSWEB.Infrastructure.Extensions;
using IKARUSWEB.Application.Commands.CreateTenant;
using IKARUSWEB.Application.Mapping;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Builder;



var builder = WebApplication.CreateBuilder(args);

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

