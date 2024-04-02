using Microsoft.AspNetCore.Mvc;

namespace WebSites.Api.Controllers;

[ApiController]
[Route("with-api-controller-attribute/with-parameter")]
public sealed class WithApiControllerAttributeAndWithParameterController
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
