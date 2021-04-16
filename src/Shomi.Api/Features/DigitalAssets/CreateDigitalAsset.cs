using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Shomi.Api.Models;
using Shomi.Api.Core;
using Shomi.Api.Interfaces;

namespace Shomi.Api.Features
{
    public class CreateDigitalAsset
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DigitalAsset).NotNull();
                RuleFor(request => request.DigitalAsset).SetValidator(new DigitalAssetValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public DigitalAssetDto DigitalAsset { get; set; }
        }

        public class Response : ResponseBase
        {
            public DigitalAssetDto DigitalAsset { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IShomiDbContext _context;

            public Handler(IShomiDbContext context)
                => _context = context;
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var digitalAsset = new DigitalAsset();

                _context.DigitalAssets.Add(digitalAsset);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    DigitalAsset = digitalAsset.ToDto()
                };
            }

        }
    }
}
