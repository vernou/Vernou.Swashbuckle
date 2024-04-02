using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.OpenApi.Models;

namespace Vernou.Swashbuckle.Tests.OperationFilters.AutomaticBadRequest;

public sealed class WithAutomaticBadRequest
{
    public static IEnumerable<object[]> OperationsWithParameter => [
        [OperationType.Get, "/with-api-controller-attribute/with-parameter"],
        [OperationType.Post, "/with-api-controller-attribute/with-parameter"],
        [OperationType.Put, "/with-api-controller-attribute/with-parameter/{id}"],
        [OperationType.Delete, "/with-api-controller-attribute/with-parameter/{id}"],
    ];

    [Theory]
    [MemberData(nameof(OperationsWithParameter))]
    public async Task HasBadRequestResponse(OperationType operationType, string path)
    {
        // Arrange

        var document = await DocumentLocator.LocateAsync();

        // Act

        var responses = document.Paths[path].Operations[operationType].Responses;

        // Assert

        responses.Keys.ShouldContain("400");
        var response = responses["400"];
        response.Content.Keys.ShouldContain("application/json");
        var content = response.Content["application/json"];
        content.Schema.Reference.Id.ShouldBe("ValidationProblemDetails");
    }

    [Theory]
    [MemberData(nameof(OperationsWithParameter))]
    public async Task CheckActionReturnBadRequest(OperationType operationType, string path)
    {
        // Arrange

        path = path.Replace("{id}", "invalid");
        if(operationType is OperationType.Get)
        {
            path += "?id=invalid";
        }
        using var factory = new WebApplicationFactory<WebSites.Api.Program>();
        using var client = factory.CreateClient();
        var httpMethod = Helper.Convert(operationType);
        var message = new HttpRequestMessage(httpMethod, path);
        if(operationType is OperationType.Put or OperationType.Post)
        {
            message.Content = new StringContent("\"invalid\"", System.Text.Encoding.UTF8, "application/json");
        }

        // Act

        using var httpResponse = await client.SendAsync(message);

        // Assert

        httpResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
    }
}
