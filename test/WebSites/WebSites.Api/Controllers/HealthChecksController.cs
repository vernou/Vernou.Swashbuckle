using Microsoft.AspNetCore.Mvc;

namespace WebSites.Api.Controllers;

[Route("health-checks")]
public class HealthChecksController
{
    [HttpGet]
    public void Get()
    { }
}
