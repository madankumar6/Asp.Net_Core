using ECommerce.UserService.Api.Middlewares;
using ECommerce.UserService.Core;
using ECommerce.UserService.Core.MappingProfiles;
using ECommerce.UserService.Infrastructure;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCoreServices();
builder.Services.AddInfrastructureServices();

// Add controllers if needed
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);
builder.Services.AddFluentValidationAutoValidation();

//Add API explorer services
builder.Services.AddEndpointsApiExplorer();

// Add swagger generation services to create swagger specification
builder.Services.AddSwaggerGen();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Build the app.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseExceptionHandlingMiddleware();

//Enable routing
app.UseRouting();

// Enable swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

//Enable authorization if needed
app.UseAuthentication();
app.UseAuthorization();

//Route requests to controllers
app.MapControllers();

app.Run();

