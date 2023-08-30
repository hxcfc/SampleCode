using CloudSignService.Application.Features.CloudSignService.Authorization.Get;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace CloudSign.Api.Controllers.CloudSignService
{
    [ApiController]
    [Route("api/cloudsign/")]
    public class AuthorizationController : BaseController
    {
        public AuthorizationController(IMediator mediator, IOptions<CasOptions> casOptions) : base(mediator, casOptions)
        {
        }

        [SwaggerOperation(
            Summary = "Gets code from Sign.",
            Description = "Requires possibility of usage app SimplySign with email and token.",
            OperationId = "CodeGet"
        )]
        [SwaggerResponse(200, "The response from Sign is okay.", typeof(object))]
        [SwaggerResponse(400, "The incoming data are invalid.", typeof(ErrorResponseModel))]
        [SwaggerResponse(401, "Unauthorized.")]
        [SwaggerResponse(403, "Forbidden.")]
        [SwaggerResponse(404, "Not Found.", typeof(ErrorResponseModel))]
        [HttpGet]
        [Route("authorization")]
        public async Task<IActionResult> CodeGet()
        {
            var query = new GetCodeQuery();
            var item = await _mediator.Send(query);

            return Ok(item);
        }
    }
}