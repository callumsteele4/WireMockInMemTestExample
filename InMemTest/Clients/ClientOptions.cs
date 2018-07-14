using Api.Clients.Users;

namespace Api.Clients
{
    public class ClientOptions
    {
        public ClientConfig UsersClient { get; set; }
        public ClientConfig PostsClient { get; set; }
    }
}