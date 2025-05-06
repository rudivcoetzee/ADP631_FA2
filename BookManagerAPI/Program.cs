using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// add db context + swagger support
builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// seed the db with 2 books
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BookDbContext>();
    db.Database.EnsureCreated();

    if (!db.Books.Any())
    {
        db.Books.AddRange(
            new Book { Title = "1984", Author = "George Orwell", Genre = "Dystopian", Year = 1949 },
            new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Classic", Year = 1960 }
        );
        db.SaveChanges();
    }
}

// get all books
app.MapGet("/books", async (BookDbContext db) =>
    await db.Books.ToListAsync());

// get book by id
app.MapGet("/books/{id}", async (int id, BookDbContext db) =>
{
    var book = await db.Books.FindAsync(id);
    return book is not null ? Results.Ok(book) : Results.NotFound();
});

// post new book
app.MapPost("/books", async (Book book, BookDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author))
        return Results.BadRequest("Title and Author are required.");

    db.Books.Add(book);
    await db.SaveChangesAsync();
    return Results.Created($"/books/{book.Id}", book);
});

// put update book
app.MapPut("/books/{id}", async (int id, Book updatedBook, BookDbContext db) =>
{
    var book = await db.Books.FindAsync(id);
    if (book is null) return Results.NotFound();

    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;
    book.Genre = updatedBook.Genre;
    book.Year = updatedBook.Year;

    await db.SaveChangesAsync();
    return Results.Ok(book);
});

// delete book
app.MapDelete("/books/{id}", async (int id, BookDbContext db) =>
{
    var book = await db.Books.FindAsync(id);
    if (book is null) return Results.NotFound();

    db.Books.Remove(book);
    await db.SaveChangesAsync();
    return Results.Ok();
});

// enable swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book API V1");
});

app.Run();
