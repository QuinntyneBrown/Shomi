using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Shomi.Api.Core;
using Shomi.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Shomi.Api.Features
{
    public class GetVideoById
    {
        public class Request: IRequest<Response>
        {
            public Guid VideoId { get; set; }
        }

        public class Response: ResponseBase
        {
            public VideoDto Video { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IShomiDbContext _context;
        
            public Handler(IShomiDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Video = (await _context.Videos.SingleOrDefaultAsync()).ToDto()
                };
            }
            
        }
    }
}
