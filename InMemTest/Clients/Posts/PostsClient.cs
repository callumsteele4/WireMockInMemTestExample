using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Api.Clients.Posts
{
    public class PostsClient
    {
        private readonly HttpClient _client;

        public PostsClient(HttpClient client, IOptions<ClientOptions> config)
        {
            _client = client;
            _client.BaseAddress = new Uri(config.Value.PostsClient.BaseAddress);
        }

        public async Task<Post> GetPostById(int id)
        {
            var response = await _client.GetAsync($"{id}");

            return await response.Content.ReadAsAsync<Post>();
        }
    }
}
