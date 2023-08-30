using Microsoft.Extensions.Options;

namespace CloudSign.Api.Controllers
{
    public class BaseController : Controller
    {
        internal readonly CasOptions _casOptions;
        internal readonly IMediator _mediator;

        public BaseController(IMediator mediator, IOptions<CasOptions> casOptions)
        {
            _mediator = mediator;
            _casOptions = casOptions.Value;
        }

        internal async Task<string> GetXmlBody(Stream bodyContent)
        {
            string xml;
            using (var reader = new StreamReader(bodyContent, Encoding.UTF8))
            {
                xml = await reader.ReadToEndAsync();
            }
            return xml;
        }
    }
}