using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace Vernou.Swashbuckle.Tests;

internal static class DocumentLocator
{
    public static Task<OpenApiDocument> LocateAsync() => _document;

    private static readonly Task<OpenApiDocument> _document = GetOpenApiDocument();

    private static async Task<OpenApiDocument> GetOpenApiDocument()
    {
        using var factory = new WebApplicationFactory<WebSites.Api.Program>();
        using var client = factory.CreateClient();
        using var httpResponse = await client.GetAsync("/swagger/v1/swagger.json");
        httpResponse.EnsureSuccessStatusCode();
        using var stream = httpResponse.Content.ReadAsStream();
        var document = new OpenApiStreamReader().Read(stream, out var diagnostic);
        diagnostic.Errors.ShouldBeEmpty();
        diagnostic.Warnings.ShouldBeEmpty();
        return document;
    }
}
