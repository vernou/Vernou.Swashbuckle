using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.OpenApi.Readers;
using WebSites.Api;

namespace Vernou.Swashbuckle.Tests;

public class OasTest
{
    [Fact]
    public async Task Retreive()
    {
        // Arrange

        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        // Act

        using var httpResponse = await client.GetAsync("/swagger/v1/swagger.json");

        // Assert

        httpResponse.EnsureSuccessStatusCode();
        using var stream = httpResponse.Content.ReadAsStream();
        var document = new OpenApiStreamReader().Read(stream, out var diagnostic);
        diagnostic.Errors.ShouldBeEmpty();
        diagnostic.Warnings.ShouldBeEmpty();
    }
}
