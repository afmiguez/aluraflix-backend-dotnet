using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Videos
{
    public class Detalhes
    {
        public class Query : IRequest<Result<VideoDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query,Result<VideoDto>>
        {
            private readonly AluraFlixContext _context;
            private readonly IMapper _mapper;

            public Handler(AluraFlixContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<VideoDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var video = await _context.Videos.SingleOrDefaultAsync(v => v.Id == request.Id);
                if (video != null)
                {
                    return Result<VideoDto>.Sucess(_mapper.Map<VideoDto>(video));
                }

                return Result<VideoDto>.Failure($"Video com id{request.Id} não encontrado",Status.NOT_FOUND);
            }
        }
    }
}