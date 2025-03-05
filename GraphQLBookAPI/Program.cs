using Microsoft.EntityFrameworkCore;
using GraphQLBooksAPI.Data;
using GraphQLBooksAPI.GraphQL.Queries;
using HotChocolate.AspNetCore;
using GraphQLBooksAPI.GraphQL.Mutations;
using GraphQLBooksAPI.GraphQL.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

// Add In-Memory Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("GraphQLBooksDB"));

// Add GraphQL Services
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAll"); // << ใช้ Policy ที่เราสร้างไว้
// Auto Migrate Database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Enable GraphQL Endpoint
app.MapGraphQL();

app.Run();