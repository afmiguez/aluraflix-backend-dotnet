using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Data;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.Videos
{
    public class Adicionar
    {
        public class Command : IRequest<Result<Video>>
        {
            public string Url { get; set; }
            public string Descricao { get; set; }
            public string Titulo { get; set; }
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
        
        public class Handler:IRequestHandler<Command,Result<Video>>
        {
            private readonly AluraFlixContext _context;

            public Handler(AluraFlixContext context)
            {
                _context = context;
            }
            
            public async Task<Result<Video>> Handle(Command request, CancellationToken cancellationToken)
            {
                var video = new Video
                {
                    Descricao = request.Descricao,
                    Titulo = request.Titulo,
                    Url = request.Url
                };
                _context.Videos.Add(video);
                var result = await _context.SaveChangesAsync() > 0;
                if (result)
                {
                    return Result<Video>.Sucess(video,Status.CREATED);
                }

                return Result<Video>.Failure("Novo video não pode ser adicionado ",Status.NOT_FOUND);
            }
        }
    }
}