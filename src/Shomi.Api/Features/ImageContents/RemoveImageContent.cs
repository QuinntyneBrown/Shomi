using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Shomi.Api.Models;
using Shomi.Api.Core;
using Shomi.Api.Interfaces;

namespace Shomi.Api.Features
{
    public class RemoveImageContent
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
                var imageContent = await _context.ImageContents.SingleAsync(x => x.ImageContentId == request.ImageContentId);
                
                _context.ImageContents.Remove(imageContent);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    ImageContent = imageContent.ToDto()
                };
            }
            
        }
    }
}
