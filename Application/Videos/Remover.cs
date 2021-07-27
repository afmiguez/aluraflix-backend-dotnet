using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Videos
{
    public class Remover
    {
        public class Command:IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly AluraFlixContext _context;

            public Handler(AluraFlixContext context)
            {
                _context = context;
            }
            
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var video = await _context.Videos.SingleOrDefaultAsync(v => v.Id == request.Id);
                if (video != null)
                {
                    _context.Videos.Remove(video);
                    var result = await _context.SaveChangesAsync() > 0;
                    if (result)
                    {
                        return Result<Unit>.Sucess(Unit.Value,Status.NO_CONTENT);
                    }

                }
                return Result<Unit>.Failure($"Vídeo com id {request.Id} não pode ser removido",Status.NOT_FOUND);
            }
        }
    }
}