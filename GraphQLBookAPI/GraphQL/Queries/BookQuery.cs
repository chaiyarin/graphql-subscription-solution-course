using GraphQLBooksAPI.Data;
using GraphQLBooksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLBooksAPI.GraphQL.Queries;

public class BookQuery
{
    public async Task<List<Book>> GetBooksAsync([Service] AppDbContext context)
    {
        return await context.Books.Include(b => b.Author).Where(b => !b.IsDeleted).ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int bookId, [Service] AppDbContext context)
    {
        return await context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.BookId == bookId && !b.IsDeleted);
    }
}