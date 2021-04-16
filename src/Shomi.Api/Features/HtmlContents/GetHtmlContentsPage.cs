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
    public class GetHtmlContentsPage
    {
        public class Request: IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response: ResponseBase
        {
            public int Length { get; set; }
            public List<HtmlContentDto> Entities { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IShomiDbContext _context;
        
            public Handler(IShomiDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from htmlContent in _context.HtmlContents
                    select htmlContent;
                
                var length = await _context.HtmlContents.CountAsync();
                
                var htmlContents = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto()).ToListAsync();
                
                return new()
                {
                    Length = length,
                    Entities = htmlContents
                };
            }
            
        }
    }
}