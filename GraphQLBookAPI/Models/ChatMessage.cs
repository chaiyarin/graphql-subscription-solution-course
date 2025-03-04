namespace GraphQLBooksAPI.Models;

public class ChatMessage
{
    public string SenderUsername { get; set; } = String.Empty;
    public string ReceiverUsername { get; set; } = String.Empty;
    public string Message { get; set; } = String.Empty;

}