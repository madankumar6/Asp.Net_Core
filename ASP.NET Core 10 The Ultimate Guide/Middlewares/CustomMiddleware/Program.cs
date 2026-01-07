using CustomMiddleware;
using Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<Middlewares.CustomMiddleware>();

var app = builder.Build();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hi");
    await next(context);
});

//app.UseMiddleware<CustomMiddleware>();

app.UseCustomMiddleware();

app.UseFullNameMiddleware();

app.UseWhen(context => context.Request.Query.ContainsKey("username"), app =>
{
    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("\nHello user, this message is from custom branch");
        await next(context);
        await context.Response.WriteAsync("\ncustom branch is completed");
    });
});

//app.Run(async (HttpContext context) =>
//{
//    await context.Response.WriteAsync("\nworld!");
//});

app.Run(async context =>
{
    await context.Response.WriteAsync("\nHello from middleware at main chain");
});

app.Run();
