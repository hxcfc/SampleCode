namespace CloudSign.Api.Controllers.Diagnostic
{
    [ApiController]
    [Route("api/cloudsign/[controller]")]
    public class ExperimentalController : Controller
    {

        public ExperimentalController()
        {
        }

        [HttpGet]
        public IActionResult GetInfo()
        {
            return Ok("Nothing special");
        }
    }
}