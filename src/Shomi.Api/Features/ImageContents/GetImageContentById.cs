using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Shomi.Api.Core;
using Shomi.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Shomi.Api.Features
{
    public class GetImageContentById
    {
        public class Request: IRequest<Response>
        {
            public Guid ImageContentId { get; set; }
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
                return new () {
                    ImageContent = (await _context.ImageContents.SingleOrDefaultAsync()).ToDto()
                };
            }
            
        }
    }
}
