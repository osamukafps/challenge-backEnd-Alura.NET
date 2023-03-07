using AluraFlixChallenge.API.Context;
using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Entities;
using AutoMapper;
using MongoDB.Driver;

namespace AluraFlixChallenge.API.Repository
{
    public class VideoRepository : IVideoRepository
    {
        private readonly MongoDbContext _context;
        private readonly IMapper _mapper;

        public VideoRepository(MongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<VideoDTO>> GetVideos()
        {
            try
            {
                Func<List<VideoDTO>> GetAllVideos = () =>
                {
                    var videosList = _context.Videos.Find(x => true).ToList();

                    if (videosList.Count == 0)
                        return new List<VideoDTO>();

                    return _mapper.Map<List<VideoDTO>>(videosList);
                };
                
                return GetAllVideos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<VideoDTO>();
            }
        }
    }
}
