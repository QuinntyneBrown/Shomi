using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Shomi.Api.Core;
using Shomi.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Shomi.Api.Features
{
    public class UpdateVideo
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Video).NotNull();
                RuleFor(request => request.Video).SetValidator(new VideoValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public VideoDto Video { get; set; }
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
                var video = await _context.Videos.SingleAsync(x => x.VideoId == request.Video.VideoId);
                
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response()
                {
                    Video = video.ToDto()
                };
            }
            
        }
    }
}
