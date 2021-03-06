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
    public class RemoveVideo
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
                var video = await _context.Videos.SingleAsync(x => x.VideoId == request.VideoId);
                
                _context.Videos.Remove(video);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Video = video.ToDto()
                };
            }
            
        }
    }
}
