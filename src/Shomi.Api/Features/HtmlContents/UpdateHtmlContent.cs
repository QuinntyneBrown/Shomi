using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Shomi.Api.Core;
using Shomi.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Shomi.Api.Features
{
    public class UpdateHtmlContent
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.HtmlContent).NotNull();
                RuleFor(request => request.HtmlContent).SetValidator(new HtmlContentValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public HtmlContentDto HtmlContent { get; set; }
        }

        public class Response: ResponseBase
        {
            public HtmlContentDto HtmlContent { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IShomiDbContext _context;
        
            public Handler(IShomiDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var htmlContent = await _context.HtmlContents.SingleAsync(x => x.HtmlContentId == request.HtmlContent.HtmlContentId);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    HtmlContent = htmlContent.ToDto()
                };
            }
            
        }
    }
}
