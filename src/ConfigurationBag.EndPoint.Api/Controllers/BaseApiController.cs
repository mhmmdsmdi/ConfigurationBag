using System.Security.Claims;
using ConfigurationBag.EndPoint.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationBag.EndPoint.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ApiResultFilter]
public class BaseApiController : ControllerBase
{
    public bool UserIsAuthenticated => HttpContext.User.Identity is { IsAuthenticated: true };

    protected Guid UserId => User.Claims.Any(x => x.Type == ClaimTypes.NameIdentifier) ? Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value) : Guid.Empty;
    protected string UserName => User.Claims.Any(x => x.Type == ClaimTypes.GivenName) ? User.Claims.First(x => x.Type == ClaimTypes.GivenName).Value : "";
}