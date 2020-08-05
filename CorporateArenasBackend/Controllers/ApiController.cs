using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorporateArenasBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public abstract class ApiController : ControllerBase
    {
    }
}