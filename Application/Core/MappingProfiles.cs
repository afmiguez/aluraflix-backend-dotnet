using Application.DTOs;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Video, VideoDto>();
            CreateMap<VideoDto,Video>();
        }
    }
}