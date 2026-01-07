using CustomAuthMiddleware;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<CustomAuthMiddleware.CustomAuthMiddleware>();
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.UseMiddleware<CustomAuthMiddleware.CustomAuthMiddleware>();

//Invoking custom middleware
app.UseLoginMiddleware();

app.Run(async context => {
    await context.Response.WriteAsync("No response");
});

app.Run();
