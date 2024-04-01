using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.OpenApi.Models;

namespace Vernou.Swashbuckle.Tests.OperationFilters.AutomaticBadRequest;

public sealed class WithoutAutomaticBadRequest
{
    public static IEnumerable<object[]> Operations => [
        [OperationType.Get, "/without-api-controller-attribute/with-parameter"],
        [OperationType.Post, "/without-api-controller-attribute/with-parameter"],
        [OperationType.Put, "/without-api-controller-attribute/with-parameter/{id}"],
        [OperationType.Delete, "/without-api-controller-attribute/with-parameter/{id}"],
        [OperationType.Get, "/with-api-controller-attribute/without-parameter"],
        [OperationType.Post, "/with-api-controller-attribute/without-parameter"],
        [OperationType.Put, "/with-api-controller-attribute/without-parameter"],
        [OperationType.Delete, "/with-api-controller-attribute/without-parameter"],
    ];

    [Theory]
    [MemberData(nameof(Operations))]
    public async Task HasNotBadRequestResponse(OperationType operationType, string path)
    {
        // Arrange

        var document = await DocumentLocator.LocateAsync();

        // Act

        var responses = document.Paths[path].Operations[operationType].Responses;

        // Assert

        responses.Keys.ShouldNotContain("400");
    }

    [Theory]
    [MemberData(nameof(Operations))]
    public async Task CheckActionReturnOk(OperationType operationType, string path)
    {
        // Arrange

        path = path.Replace("{id}", "42");
        if(operationType is OperationType.Get)
        {
            path += "?id=42";
        }
        using var factory = new WebApplicationFactory<WebSites.Api.Program>();
        using var client = factory.CreateClient();
        var httpMethod = Helper.Convert(operationType);
        var message = new HttpRequestMessage(httpMethod, path);
        if(operationType is OperationType.Put or OperationType.Post)
        {
            message.Content = new StringContent("""{"id":42}""", System.Text.Encoding.UTF8, "application/json");
        }

        // Act

        using var httpResponse = await client.SendAsync(message);

        // Assert

        httpResponse.EnsureSuccessStatusCode();
    }
}
