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
    public class RemoveContent
    {
        public class Request: IRequest<Response>
        {
            public Guid ContentId { get; set; }
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
                var content = await _context.Contents.SingleAsync(x => x.ContentId == request.ContentId);
                
                _context.Contents.Remove(content);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Content = content.ToDto()
                };
            }
            
        }
    }
}
