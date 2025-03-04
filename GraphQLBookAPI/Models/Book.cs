namespace GraphQLBooksAPI.Models;

public class Book
{
    public int BookId { get; set; }
    public string BookName { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    public string Description { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public decimal Price { get; set; }
    public int Rating { get; set; }
    public string CoverBookImageUrl { get; set; } = string.Empty;

    public Author? Author { get; set; }
}