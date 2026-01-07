var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run(async (HttpContext context) =>
//{
//    await context.Response.WriteAsync("Hello\n");
//});

//app.Run(async (HttpContext context) =>
//{
//    await context.Response.WriteAsync(" world!\n");
//});

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hi");
    await next(context);
});

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello");
    //await next(context);
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync(" world!\n");
});

app.Run();
