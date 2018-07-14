using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Clients.Users;
using Api.Test.Collections;
using Api.Test.Collections.WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Api.Test.Controllers
{

    [Collection(CollectionType.WireMock)]
    public class UsersControllerTests : IDisposable
    {
        private readonly HttpClient _client;
        private readonly FluentMockServer _usersServer;

        public UsersControllerTests(WireMockFixture wireMockFixture)
        {
            _client = wireMockFixture.Client;
            _usersServer = wireMockFixture.UsersServer;
        }

        [Fact]
        public async Task GetUsers_GivenId_ReturnsValue()
        {
            const string expectedUsername = "Bob";
            _usersServer
                .Given(Request.Create().WithPath("/users/1").UsingGet())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithHeader("content-type", "application/json")
                        .WithBodyAsJson(new User {Username = expectedUsername}));

            var response = await _client.GetAsync("/api/users/1");
            var user = await response.Content.ReadAsAsync<User>();

            Assert.NotNull(user);
            Assert.Equal(expectedUsername, user.Username);
        }

        [Fact]
        public async Task GetUsers_GivenDifferentId_ReturnsDifferentValue()
        {
            const string expectedUsername = "Pete";
            _usersServer
                .Given(Request.Create().WithPath("/users/2").UsingGet())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithHeader("content-type", "application/json")
                        .WithBodyAsJson(new User { Username = expectedUsername }));

            var response = await _client.GetAsync("/api/users/2");
            var user = await response.Content.ReadAsAsync<User>();

            Assert.NotNull(user);
            Assert.Equal(expectedUsername, user.Username);
        }

        public void Dispose()
        {
            _usersServer.Reset();
        }
    }
}