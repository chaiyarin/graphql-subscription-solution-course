namespace GraphQLBooksAPI.GraphQL.Mutations;

public class Mutation
{
    public AuthorMutation AuthorMutation => new(); // ✅ เพิ่ม AuthorMutation
    public BookMutation BookMutation => new(); // ✅ เพิ่ม BookMutation
    public ChatMutation ChatMutation => new(); // ✅ เพิ่ม ChatMutation
}