namespace CloudSign.Api.Controllers.Diagnostic
{
    [ApiController]
    [Route("api/cloudsign/[controller]")]
    public class VersionInfoController : Controller
    {
        [HttpGet]
        public IActionResult GetInfo()
        {
            return Ok($"{CloudSignServiceApiVersion.Name} {CloudSignServiceApiVersion.Version}");
        }
    }
}