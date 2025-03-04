using GraphQLBooksAPI.Models;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLBooksAPI.GraphQL.Subscriptions;

public class Subscription
{
    // ✅ Dynamic Topic สำหรับ Chat Messages
    [SubscribeAndResolve]
    [Topic($"MessageTo_{{receiverUsername}}")] // ✅ ใช้ Dynamic Topic ที่ Method
    public async ValueTask<ISourceStream<ChatMessage>> SubscribeToMessages(
        string receiverUsername,
        [Service] ITopicEventReceiver eventReceiver,
        CancellationToken cancellationToken)
    {
        string topic = $"MessageTo_{receiverUsername}";
        return await eventReceiver.SubscribeAsync<ChatMessage>(topic, cancellationToken);
    }

    // ✅ Subscription สำหรับ Author Events
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