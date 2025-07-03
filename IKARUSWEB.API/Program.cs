using IKARUSWEB.Infrastructure.Extensions;
using IKARUSWEB.Application.Commands.CreateTenant;
using IKARUSWEB.Application.Mapping;
using FluentValidation.AspNetCore;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Diğer servisler...
builder.Services.AddInfrastructure(builder.Configuration);

// Add MediatR
builder.Services.AddMediatR(cfg =>
{
    // Handlers, pre/post processors, vb. bu assembly’den taransın
    cfg.RegisterServicesFromAssemblyContaining<CreateTenantCommandHandler>();
});
// AutoMapper
builder.Services.AddAutoMapper(typeof(TenantProfile).Assembly);

// 2) MVC + FluentValidation
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();         // ← IServiceCollection uzantısı
builder.Services.AddFluentValidationClientsideAdapters();    // ← opsiyonel, client-side doğrulama

// 3) Validator’ları kaydet
builder.Services.AddValidatorsFromAssemblyContaining<CreateTenantCommandValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();





app.Run();

