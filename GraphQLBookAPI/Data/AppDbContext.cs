using Microsoft.EntityFrameworkCore;
using GraphQLBooksAPI.Models;

namespace GraphQLBooksAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed Authors
        modelBuilder.Entity<Author>().HasData(
            new Author { AuthorId = 1, Nickname = "J.K.", Firstname = "Joanne", Lastname = "Rowling", BirthdayDate = new DateTime(1965, 7, 31).ToString(), CoverProfileImageUrl = "https://example.com/jk.jpg" },
            new Author { AuthorId = 2, Nickname = "GRRM", Firstname = "George", Lastname = "Martin", BirthdayDate = new DateTime(1948, 9, 20).ToString(), CoverProfileImageUrl = "https://example.com/grrm.jpg" },
            new Author { AuthorId = 3, Nickname = "Tolkien", Firstname = "J.R.R.", Lastname = "Tolkien", BirthdayDate = new DateTime(1892, 1, 3).ToString(), CoverProfileImageUrl = "https://example.com/tolkien.jpg" },
            new Author { AuthorId = 4, Nickname = "King", Firstname = "Stephen", Lastname = "King", BirthdayDate = new DateTime(1947, 9, 21).ToString(), CoverProfileImageUrl = "https://example.com/king.jpg" },
            new Author { AuthorId = 5, Nickname = "Asimov", Firstname = "Isaac", Lastname = "Asimov", BirthdayDate = new DateTime(1920, 1, 2).ToString(), CoverProfileImageUrl = "https://example.com/asimov.jpg" }
        );

        // Seed Books
        modelBuilder.Entity<Book>().HasData(
            new Book { BookId = 1, BookName = "Harry Potter", AuthorId = 1, Price = 20.99m, Rating = 5, CoverBookImageUrl = "https://example.com/hp.jpg" },
            new Book { BookId = 2, BookName = "Game of Thrones", AuthorId = 2, Price = 25.50m, Rating = 5, CoverBookImageUrl = "https://example.com/got.jpg" },
            new Book { BookId = 3, BookName = "Lord of the Rings", AuthorId = 3, Price = 30.00m, Rating = 5, CoverBookImageUrl = "https://example.com/lotr.jpg" },
            new Book { BookId = 4, BookName = "The Shining", AuthorId = 4, Price = 15.75m, Rating = 4, CoverBookImageUrl = "https://example.com/shining.jpg" },
            new Book { BookId = 5, BookName = "Foundation", AuthorId = 5, Price = 18.20m, Rating = 5, CoverBookImageUrl = "https://example.com/foundation.jpg" }
        );
    }
}