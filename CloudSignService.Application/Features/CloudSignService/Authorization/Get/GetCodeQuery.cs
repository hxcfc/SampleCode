using MediatR;

namespace CloudSignService.Application.Features.CloudSignService.Authorization.Get
{
    public class GetCodeQuery : IRequest<object?>
    {
        public GetCodeQuery()
        {
        }
    }
}