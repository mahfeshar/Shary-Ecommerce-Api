using Microsoft.AspNetCore.Mvc;
using Shary.API.Errors;

namespace Shary.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    public ActionResult Error(int code)
    {
        return NotFound(new ApiResponse(code));
    }
}
