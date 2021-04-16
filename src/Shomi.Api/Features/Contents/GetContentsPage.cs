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
    public class GetContentsPage
    {
        public class Request: IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response: ResponseBase
        {
            public int Length { get; set; }
            public List<ContentDto> Entities { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IShomiDbContext _context;
        
            public Handler(IShomiDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from content in _context.Contents
                    select content;
                
                var length = await _context.Contents.CountAsync();
                
                var contents = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto()).ToListAsync();
                
                return new()
                {
                    Length = length,
                    Entities = contents
                };
            }
            
        }
    }
}
