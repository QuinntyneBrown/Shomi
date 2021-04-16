using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Shomi.Api.Core;
using Shomi.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Shomi.Api.Features
{
    public class UpdateContent
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Content).NotNull();
                RuleFor(request => request.Content).SetValidator(new ContentValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public ContentDto Content { get; set; }
        }

        public class Response: ResponseBase
        {
            public ContentDto Content { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IShomiDbContext _context;
        
            public Handler(IShomiDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var content = await _context.Contents.SingleAsync(x => x.ContentId == request.Content.ContentId);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Content = content.ToDto()
                };
            }
            
        }
    }
}
