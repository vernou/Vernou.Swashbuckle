namespace Vernou.Swashbuckle.Tests;

public sealed class OasTest
{
    [Fact]
    public async Task Retreive()
    {
        var document = await DocumentLocator.LocateAsync();
        document.ShouldNotBeNull();
    }
}
