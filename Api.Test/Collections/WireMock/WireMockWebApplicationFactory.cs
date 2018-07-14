using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using WireMock.Server;

namespace Api.Test.Collections.WireMock
{
    public class WireMockWebApplicationFactory : WebApplicationFactory<Startup>
    {
        public FluentMockServer UsersServer { get; }
        public FluentMockServer PostsServer { get; }

        public WireMockWebApplicationFactory()
        {
            UsersServer = FluentMockServer.Start();
            PostsServer = FluentMockServer.Start();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                var testConfiguration = new Dictionary<string, string>
                {
                    {"Clients:UsersClient:BaseAddress", $"http://localhost:{UsersServer.Ports.Single()}/users/"},
                    {"Clients:PostsClient:BaseAddress", $"http://localhost:{PostsServer.Ports.Single()}/posts/"}
                };
                configurationBuilder.AddInMemoryCollection(testConfiguration);
            });
        }

        public new void Dispose()
        {
            UsersServer.Dispose();
            PostsServer.Dispose();
            base.Dispose();
        }
    }
}