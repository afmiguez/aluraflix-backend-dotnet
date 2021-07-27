using System;
using Domain;
using FluentValidation;

namespace Application.Videos
{
    public class VideoValidator:AbstractValidator<Video>
    {
        public VideoValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty();
            RuleFor(x => x.Url).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.Url));
            RuleFor(x => x.Titulo).NotEmpty();
        }
    }
}