using GraphQLBooksAPI.Data;
using GraphQLBooksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLBooksAPI.GraphQL.Queries;

public class AuthorQuery
{
    public async Task<List<Author>> GetAuthorsAsync([Service] AppDbContext context)
    {
        return await context.Authors.Include(e => e.Books).Where(a => !a.IsDeleted).ToListAsync();
    }

    public async Task<Author?> GetAuthorByIdAsync(int authorId, [Service] AppDbContext context)
    {
        return await context.Authors.Include(e => e.Books).FirstOrDefaultAsync(a => a.AuthorId == authorId && !a.IsDeleted);
    }
}