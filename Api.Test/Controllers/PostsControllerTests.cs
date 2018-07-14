using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Clients.Posts;
using Api.Test.Collections;
using Api.Test.Collections.WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Api.Test.Controllers
{
    [Collection(CollectionType.WireMock)]
    public class PostsControllerTests : IDisposable
    {
        private readonly HttpClient _client;
        private readonly FluentMockServer _postsServer;

        public PostsControllerTests(WireMockFixture wireMockFixture)
        {
            _client = wireMockFixture.Client;
            _postsServer = wireMockFixture.PostsServer;
        }

        [Fact]
        public async Task GetPosts_GivenId_ReturnsValue()
        {
            const string expectedTitle = "First title";
            _postsServer
                .Given(Request.Create().WithPath("/posts/1").UsingGet())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithHeader("content-type", "application/json")
                        .WithBodyAsJson(new Post { Title = expectedTitle }));

            var response = await _client.GetAsync("/api/posts/1");
            var post = await response.Content.ReadAsAsync<Post>();

            Assert.NotNull(post);
            Assert.Equal(expectedTitle, post.Title);
        }

        [Fact]
        public async Task GetPosts_GivenDifferentId_ReturnsDifferentValue()
        {
            const string expectedTitle = "Second title";
            _postsServer
                .Given(Request.Create().WithPath("/posts/2").UsingGet())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithHeader("content-type", "application/json")
                        .WithBodyAsJson(new Post { Title = expectedTitle }));

            var response = await _client.GetAsync("/api/posts/2");
            var post = await response.Content.ReadAsAsync<Post>();

            Assert.NotNull(post);
            Assert.Equal(expectedTitle, post.Title);
        }

        public void Dispose()
        {
            _postsServer.Reset();
        }
    }
}