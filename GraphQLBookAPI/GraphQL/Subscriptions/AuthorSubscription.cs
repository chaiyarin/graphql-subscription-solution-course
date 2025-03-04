using GraphQLBooksAPI.Models;

namespace GraphQLBooksAPI.GraphQL.Subscriptions;

public class AuthorSubscription
{
    [Subscribe]
    [Topic("AuthorCreated")]
    public Author OnAuthorCreated([EventMessage] Author author) => author;

    [Subscribe]
    [Topic("AuthorUpdated")]
    public Author OnAuthorUpdated([EventMessage] Author author) => author;

    [Subscribe]
    [Topic("AuthorDeleted")]
    public int OnAuthorDeleted([EventMessage] int authorId) => authorId;
}