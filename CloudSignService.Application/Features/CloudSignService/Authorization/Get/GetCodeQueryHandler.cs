using CloudSignService.Application.Interfaces.Authorization;
using MediatR;

namespace CloudSignService.Application.Features.CloudSignService.Authorization.Get
{
    public class GetCodeQueryHandler : IRequestHandler<GetCodeQuery, object?>
    {
        private readonly IAuthorization _iAuthorization;

        public GetCodeQueryHandler(IAuthorization iAuthorization)
        {
            _iAuthorization = iAuthorization;
        }

        public async Task<object?> Handle(GetCodeQuery request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}