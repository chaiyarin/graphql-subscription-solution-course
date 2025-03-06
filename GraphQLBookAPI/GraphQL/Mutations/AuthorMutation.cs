using GraphQLBooksAPI.Data;
using GraphQLBooksAPI.Models;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GraphQLBooksAPI.GraphQL.Mutations;

public class AuthorMutation
{
    public async Task<Author> CreateAuthorAsync(
        string nickname,
        string firstname,
        string lastname,
        string birthdayDate,
        string coverProfileImageUrl,
        [Service] AppDbContext context,
        [Service] ITopicEventSender eventSender)
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
        await eventSender.SendAsync("AuthorCreated", author);
        return author;
    }

    public async Task<Author?> UpdateAuthorAsync(
    int authorId,
    string? nickname,
    string? firstname,
    string? lastname,
    string? birthdayDate,
    string? coverProfileImageUrl,
    [Service] AppDbContext context)
    {
        var author = await context.Authors.FirstOrDefaultAsync(a => a.AuthorId == authorId);
        if (author == null)
        {
            throw new Exception($"Author with ID {authorId} not found.");
        }

        // อัปเดตเฉพาะค่าที่ส่งมา (ค่าที่ไม่ส่งมา จะไม่เปลี่ยนแปลง)
        if (!string.IsNullOrEmpty(nickname)) author.Nickname = nickname;
        if (!string.IsNullOrEmpty(firstname)) author.Firstname = firstname;
        if (!string.IsNullOrEmpty(lastname)) author.Lastname = lastname;
        if (!string.IsNullOrEmpty(birthdayDate)) author.BirthdayDate = birthdayDate;
        if (!string.IsNullOrEmpty(coverProfileImageUrl)) author.CoverProfileImageUrl = coverProfileImageUrl;

        author.UpdatedDate = DateTime.UtcNow; // ✅ เพิ่ม timestamp อัปเดต

        await context.SaveChangesAsync();
        return author;
    }

    public async Task<Author> DeleteAuthorAsync(int authorId, [Service] AppDbContext context)
    {
        var author = await context.Authors.FindAsync(authorId);
        if (author == null || author.IsDeleted)
        {
            throw new Exception($"Author with ID {authorId} not found or already deleted.");
        }

        author.IsDeleted = true;
        author.UpdatedDate = DateTime.UtcNow; // Update the timestamp

        await context.SaveChangesAsync();
        return author;
    }
}