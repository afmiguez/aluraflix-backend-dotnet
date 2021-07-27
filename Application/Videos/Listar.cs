using System.Collections.Generic;
using System.Linq;
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
    public class Listar
    {
        public class Query:IRequest<Result<IList<VideoDto>>>
        {
            
        }

        public class Handler : IRequestHandler<Query, Result<IList<VideoDto>>>
        {
            private readonly AluraFlixContext _context;
            private readonly IMapper _mapper;

            public Handler(AluraFlixContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            
            public async Task<Result<IList<VideoDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var videos = await _context.Videos.ToListAsync();
                var videosDtos = videos.Select(video => _mapper.Map<VideoDto>(video)).ToList();
                    
                return Result<IList<VideoDto>>.Sucess(videosDtos);
            }
        }
    }
}