using GraphQLBooksAPI.Models;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLBooksAPI.GraphQL.Subscriptions;

public class ChatSubscription
{
    [SubscribeAndResolve]
    [Topic($"MessageTo_{{receiverUsername}}")] // ✅ ใช้ Dynamic Topic แบบถูกต้อง
    public async ValueTask<ISourceStream<ChatMessage>> SubscribeToMessages(
        string receiverUsername, // ✅ พารามิเตอร์ถูกต้อง
        [Service] ITopicEventReceiver eventReceiver,
        CancellationToken cancellationToken)
    {
        string topic = $"MessageTo_{receiverUsername}";
        return await eventReceiver.SubscribeAsync<ChatMessage>(topic, cancellationToken);
    }
}