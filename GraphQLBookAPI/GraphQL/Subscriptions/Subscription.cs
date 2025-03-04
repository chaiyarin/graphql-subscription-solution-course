using GraphQLBooksAPI.Models;

namespace GraphQLBooksAPI.GraphQL.Subscriptions;

public class Subscription
{
    public AuthorSubscription AuthorSubscription => new(); // ✅ เพิ่ม AuthorSubscription
    public ChatSubscription ChatSubscription => new(); // ✅ เพิ่ม ChatSubscription
}