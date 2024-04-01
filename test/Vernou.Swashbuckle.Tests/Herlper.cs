using Microsoft.OpenApi.Models;

namespace Vernou.Swashbuckle.Tests;

internal static class Helper
{
    public static HttpMethod Convert(OperationType operationType)
    {
        return operationType switch {
            OperationType.Get => HttpMethod.Get,
            OperationType.Post => HttpMethod.Post,
            OperationType.Put => HttpMethod.Put,
            OperationType.Delete => HttpMethod.Delete,
            _ => throw new ArgumentOutOfRangeException(nameof(operationType))
        };
    }
}
