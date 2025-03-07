using GraphQLBooksAPI.Data;
using GraphQLBooksAPI.Models;
using HotChocolate.Subscriptions;

namespace GraphQLBooksAPI.GraphQL.Mutations;
public class BookMutation
{
    public async Task<Book> CreateBookAsync(
        string bookName,
        string description,
        decimal price,
        int authorId,
        int rating,
        string coverImageUrl,
        [Service] AppDbContext context,
        [Service] ITopicEventSender eventSender)
    {
        var book = new Book
        {
            BookName = bookName,
            Description = description,
            Price = price,
            AuthorId = authorId,
            Rating = rating,
            CoverBookImageUrl = coverImageUrl,
            IsDeleted = false
        };

        context.Books.Add(book);
        await context.SaveChangesAsync();
        await eventSender.SendAsync("BookCreated", book);
        return book;
    }

    public async Task<Book?> UpdateBookAsync(
        int bookId,
        string? bookName,
        string? description,
        decimal? price,
        int? rating,
        int? authorId,
        string? coverImageUrl,
        [Service] AppDbContext context,
        [Service] ITopicEventSender eventSender)
    {
        var book = await context.Books.FindAsync(bookId);
        if (book == null || book.IsDeleted)
        {
            throw new Exception($"Book with ID {bookId} not found or already deleted.");
        }

        if (!string.IsNullOrEmpty(bookName)) book.BookName = bookName;
        if (!string.IsNullOrEmpty(description)) book.Description = description;
        if (!string.IsNullOrEmpty(description)) book.Description = description;
        if (price !=  null) book.Price = price.Value;
        if (rating != null) book.Rating = rating.Value;
        if (authorId != null) book.AuthorId = authorId.Value;
        if (!string.IsNullOrEmpty(coverImageUrl)) book.CoverBookImageUrl = coverImageUrl;

        book.UpdatedDate = DateTime.UtcNow;
        await eventSender.SendAsync("BookUpdated", book);

        await context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> DeleteBookAsync(int bookId, [Service] AppDbContext context,    [Service] ITopicEventSender eventSender)
    {
        var book = await context.Books.FindAsync(bookId);
        if (book == null || book.IsDeleted)
        {
            throw new Exception($"Book with ID {bookId} not found or already deleted.");
        }

        book.IsDeleted = true;
        book.UpdatedDate = DateTime.UtcNow;
        await eventSender.SendAsync("BookDeleted",bookId);

        await context.SaveChangesAsync();
        return book;
    }
}