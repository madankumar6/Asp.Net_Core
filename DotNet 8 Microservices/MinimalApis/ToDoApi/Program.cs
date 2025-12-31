using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DI services here - Add()
builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseInMemoryDatabase("ToDoDatabase"));

var app = builder.Build();

// Add middleware here, configure pipeline - Use() and Map()
app.MapGet("/todoItems", async (ToDoDbContext db) =>
    await db.ToDos.ToListAsync());

app.MapGet("/todoItems/{id}", async (int id, ToDoDbContext db) =>
    await db.ToDos.FindAsync(id));

app.MapPost("/todoItems", async (ToDoApi.Models.ToDoItem toDoItem, ToDoDbContext db) =>
{
    db.ToDos.Add(toDoItem);
    await db.SaveChangesAsync();
    return Results.Created($"/todoItems/{toDoItem.Id}", toDoItem);
});

app.MapPut("/todoItems/{id}", async (int id, ToDoApi.Models.ToDoItem updatedItem, ToDoDbContext db) =>
{
    var toDoItem = await db.ToDos.FindAsync(id);
    if (toDoItem is null) return Results.NotFound();
    toDoItem.Title = updatedItem.Title;
    toDoItem.IsCompleted = updatedItem.IsCompleted;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoItems/{id}", async (int id, ToDoDbContext db) =>
{
    var toDoItem = await db.ToDos.FindAsync(id);
    if (toDoItem is null) return Results.NotFound();
    db.ToDos.Remove(toDoItem);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
