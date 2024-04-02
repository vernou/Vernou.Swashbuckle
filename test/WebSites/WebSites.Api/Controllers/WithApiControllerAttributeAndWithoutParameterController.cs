using Microsoft.AspNetCore.Mvc;

namespace WebSites.Api.Controllers;

[ApiController]
[Route("with-api-controller-attribute/without-parameter")]
public sealed class WithApiControllerAttributeAndWithoutParameterController
{
    [HttpGet]
    public void Get() { }

    [HttpPost]
    public void Post() { }

    [HttpPut]
    public void Put() { }

    [HttpDelete]
    public void Delete() { }
}
