namespace GraphQLBooksAPI.Models;

public class Author
{
    public int AuthorId { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string BirthdayDate { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string CoverProfileImageUrl { get; set; } = string.Empty;

    public List<Book>? Books { get; set; }
}