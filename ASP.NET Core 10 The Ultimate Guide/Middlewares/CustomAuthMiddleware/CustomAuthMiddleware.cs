
using Microsoft.AspNetCore.WebUtilities;

namespace CustomAuthMiddleware
{
    public class CustomAuthMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Method == "GET" && context.Request.Path == "/")
            {
                context.Response.StatusCode = 200;
            }
            else if (context.Request.Method == "POST" && context.Request.Path == "/")
            {
                var body = new StreamReader(context.Request.Body);
                var bodyContent = await body.ReadToEndAsync();
                var data = QueryHelpers.ParseQuery(bodyContent);

                string email = string.Empty;
                string password = string.Empty;

                //read 'email' if submitted in the request body
                if (data.ContainsKey("email"))
                {
                    email = data["email"][0];
                }
                else
                {
                    if (context.Response.StatusCode == 200)
                        context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid input for 'email'\n");
                }

                //read 'password' if submitted in the request body
                if (data.ContainsKey("password"))
                {
                    password = data["password"][0];
                }
                else
                {
                    if (context.Response.StatusCode == 200)
                        context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid input for 'password'\n");
                }

                if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
                {
                    if (email == "admin@example.com" && password == "admin1234")
                    {
                        context.Response.StatusCode = 200;
                        await context.Response.WriteAsync("Successful login\n");
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid login\n");
                    }
                }
            }
        }
    }
}
