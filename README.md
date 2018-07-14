# In-Memory Testing ASP.NET Core 2.1 with WireMock

This is an example project structure for testing an in-memory ASP.NET Core 2.1 server with WireMock.

Testing with an in-memory server and WireMock allows you to ensure that you are testing only your application, and not also the web APIs the application depends on.

## InMemTest

This is the ASP.NET Core 2.1 API which we will be testing.

It contains two controllers, [UsersController.cs](/Controllers/UsersController) and [PostsController.cs](/Controllers/PostsController), which expose simple actions to retrieve either a [User](/Clients/Users/User) or a [Post](/Clients/Posts/Post).

These controllers call clients, registered in the [Startup.cs](/Startup), which take their configuration from [appsettings.json](/appsettings.json).

These clients are pointing at [JSONPlaceholder](https://jsonplaceholder.typicode.com/) which provides some dummy user and post data.

## Api.Test

This is the test project, through which we are testing the controllers.

Each controller has a respective test class, see [UsersControllerTests.cs](/Controllers/UsersControllerTests) and [PostsControllerTests.cs](/Controllers/PostsControllerTests) respectively.

These test classes share a [Collection Fixture](https://xunit.github.io/docs/shared-context#collection-fixture), which allows them to share a common context.

The context in this case is held in [WireMockFixture](/Collections/WireMock/WireMockFixture), which contains a reference to a HttpClient with which to call our in-memory server, as well as the two WireMock servers we are using.

To ensure that http calls will go to our WireMock servers, as opposed to JSONPlaceholder, the configuration for our in-memory server is modified in [WireMockWebApplicationFactory.cs](/Collections/WireMock/WireMockWebApplicationFactory). Here we provide alternative base addresses for the users and posts clients.

## WireMock.Net

See [here](https://github.com/WireMock-Net/WireMock.Net).