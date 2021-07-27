
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Data;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Videos
{
    public class Editar
    {

        public class Command : IRequest<Result<Video>>
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Descricao { get; set; }
            public string Url { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                
                //RuleFor(x => x.Video).SetValidator(new VideoValidator());
                RuleFor(x => new Video
                {
                    Descricao = x.Descricao,
                    Titulo = x.Titulo,
                    Url = x.Url
                }).SetValidator(new VideoValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Video>>
        {
            private readonly AluraFlixContext _context;

            public Handler(AluraFlixContext context)
            {
                _context = context;
            }

            public async Task<Result<Video>> Handle(Command request, CancellationToken cancellationToken)
            {
                Video video = await _context.Videos.SingleOrDefaultAsync(v=>v.Id==request.Id);

                if (video != null)
                {
                    video.Descricao = request.Descricao;
                    video.Titulo = request.Titulo;
                    video.Url = request.Url;

                    await _context.SaveChangesAsync(); 
                    return Result<Video>.Sucess(video);
                    
                }
                
                return Result<Video>.Failure($"Video com id {request.Id} não pode ser editado",Status.NOT_FOUND);
            }
        }
        
    }
}