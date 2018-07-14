using Xunit;

namespace Api.Test.Collections.WireMock
{
    [CollectionDefinition(CollectionType.WireMock)]
    public class WireMockCollection : ICollectionFixture<WireMockFixture>
    { }
}