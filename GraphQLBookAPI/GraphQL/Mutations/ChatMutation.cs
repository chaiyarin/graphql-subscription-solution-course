using GraphQLBooksAPI.Models;
using HotChocolate.Subscriptions;

namespace GraphQLBooksAPI.GraphQL.Mutations
{
    public class ChatMutation
    {
        public async Task<ChatMessage> SendMessage(ChatMessage message, [Service] ITopicEventSender eventSender)
        {
            await eventSender.SendAsync($"MessageTo_{message.ReceiverUsername}", message);
            return message;
        }
    }
}