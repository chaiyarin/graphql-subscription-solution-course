using Microsoft.EntityFrameworkCore;
using GraphQLBooksAPI.Data;
using GraphQLBooksAPI.GraphQL.Queries;
using HotChocolate.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add In-Memory Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("GraphQLBooksDB"));

// Add GraphQL Services
builder.Services
    .AddGraphQLServer()
    .AddQueryType<BookQuery>(); // ✅ เปลี่ยนเป็น Query หลัก

var app = builder.Build();

// Auto Migrate Database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Enable GraphQL Endpoint
app.MapGraphQL();

app.Run();