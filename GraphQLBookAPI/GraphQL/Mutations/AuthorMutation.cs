using GraphQLBooksAPI.Data;
using GraphQLBooksAPI.Models;

namespace GraphQLBooksAPI.GraphQL.Mutations;

public class AuthorMutation
{
    public async Task<Author> CreateAuthorAsync(
        string nickname,
        string firstname,
        string lastname,
        DateTime birthdayDate,
        string coverProfileImageUrl,
        [Service] AppDbContext context)
    {
        var author = new Author
        {
            Nickname = nickname,
            Firstname = firstname,
            Lastname = lastname,
            BirthdayDate = birthdayDate,
            CoverProfileImageUrl = coverProfileImageUrl,
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        };

        context.Authors.Add(author);
        await context.SaveChangesAsync();
        return author;
    }
}