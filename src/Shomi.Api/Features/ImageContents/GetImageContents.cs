using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Shomi.Api.Core;
using Shomi.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Shomi.Api.Features
{
    public class GetImageContents
    {
        public class Request: IRequest<Response> { }

        public class Response: ResponseBase
        {
            public List<ImageContentDto> ImageContents { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IShomiDbContext _context;
        
            public Handler(IShomiDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    ImageContents = await _context.ImageContents.Select(x => x.ToDto()).ToListAsync()
                };
            }
            
        }
    }
}
