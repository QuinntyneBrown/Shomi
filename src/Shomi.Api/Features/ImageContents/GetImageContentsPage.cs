using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Shomi.Api.Extensions;
using Shomi.Api.Core;
using Shomi.Api.Interfaces;
using Shomi.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Shomi.Api.Features
{
    public class GetImageContentsPage
    {
        public class Request: IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response: ResponseBase
        {
            public int Length { get; set; }
            public List<ImageContentDto> Entities { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IShomiDbContext _context;
        
            public Handler(IShomiDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from imageContent in _context.ImageContents
                    select imageContent;
                
                var length = await _context.ImageContents.CountAsync();
                
                var imageContents = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto()).ToListAsync();
                
                return new()
                {
                    Length = length,
                    Entities = imageContents
                };
            }
            
        }
    }
}
