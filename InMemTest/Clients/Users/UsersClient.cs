using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Api.Clients.Users
{
    public class UsersClient
    {
        private readonly HttpClient _client;

        public UsersClient(HttpClient client, IOptions<ClientOptions> config)
        {
            _client = client;
            _client.BaseAddress = new Uri(config.Value.UsersClient.BaseAddress);
        }

        public async Task<User> GetUserById(int id)
        {
            var response = await _client.GetAsync($"{id}");

            return await response.Content.ReadAsAsync<User>();
        }
    }
}