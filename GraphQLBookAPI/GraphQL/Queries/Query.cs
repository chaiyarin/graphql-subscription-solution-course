namespace GraphQLBooksAPI.GraphQL.Queries;

public class Query
{
    public BookQuery BooksQuery => new(); // ✅ แยก BookQuery ออกเป็นโมดูล
    public AuthorQuery AuthorsQuery => new(); // ✅ แยก AuthorQuery ออกเป็นโมดูล
}