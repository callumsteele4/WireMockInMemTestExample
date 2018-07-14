using System;
using System.Net.Http;
using WireMock.Server;

namespace Api.Test.Collections.WireMock
{
    public class WireMockFixture : IDisposable
    {
        private readonly WireMockWebApplicationFactory _factory;
        public readonly HttpClient Client;
        public readonly FluentMockServer UsersServer;
        public readonly FluentMockServer PostsServer;

        public WireMockFixture()
        {
            _factory = new WireMockWebApplicationFactory();
            Client = _factory.CreateDefaultClient();
            UsersServer = _factory.UsersServer;
            PostsServer = _factory.PostsServer;
        }

        public void Dispose()
        {
            _factory.Dispose();
        }
    }
}