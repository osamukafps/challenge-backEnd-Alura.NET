using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Entities;
using AutoMapper;

namespace AluraFlixChallenge.API.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Video, VideoDTO>().ReverseMap();
                config.CreateMap<Category, CategoryDTO>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
