using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Shomi.Api.Models;
using Shomi.Api.Core;
using Shomi.Api.Interfaces;

namespace Shomi.Api.Features
{
    public class CreateImageContent
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ImageContent).NotNull();
                RuleFor(request => request.ImageContent).SetValidator(new ImageContentValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public ImageContentDto ImageContent { get; set; }
        }

        public class Response: ResponseBase
        {
            public ImageContentDto ImageContent { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IShomiDbContext _context;
        
            public Handler(IShomiDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var imageContent = new ImageContent();
                
                _context.ImageContents.Add(imageContent);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    ImageContent = imageContent.ToDto()
                };
            }
            
        }
    }
}
