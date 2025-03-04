namespace GraphQLBooksAPI.GraphQL.Mutations;

public class Mutation
{
    public AuthorMutation AuthorMutation => new(); // ✅ เพิ่ม AuthorMutation
    public ChatMutation ChatMutation => new(); // ✅ เพิ่ม ChatMutation
}