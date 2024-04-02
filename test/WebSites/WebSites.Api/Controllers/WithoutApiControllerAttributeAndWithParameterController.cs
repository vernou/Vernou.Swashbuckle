using Microsoft.AspNetCore.Mvc;

namespace WebSites.Api.Controllers;

[Route("without-api-controller-attribute/with-parameter")]
public sealed class WithoutApiControllerAttributeAndWithParameterController
{
    [HttpGet]
    public void Get(int id) { }

    [HttpPost]
    public void Post(Dictionary<string, int> foo) { }

    [HttpPut("{id}")]
    public void Put(int id, Dictionary<string, int> foo) { }

    [HttpDelete("{id}")]
    public void Delete(int id) { }
}
