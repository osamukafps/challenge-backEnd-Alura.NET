using AluraFlixChallenge.API.Context;
using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Entities;
using AutoMapper;
using MongoDB.Bson;
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

        public async Task<VideoDTO> GetVideoById(long id)
        {
            try
            {
                var video = _context.Videos.Find<Video>(x => x.Id == id)
                                    .Project<Video>(Builders<Video>.Projection.Exclude("_id"))
                                    .FirstOrDefault();

                if (video is null)
                    return new VideoDTO();

                return _mapper.Map<VideoDTO>(video);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new VideoDTO();
            }
        }

        public async Task<bool> PostVideo(VideoDTO videoDTO)
        {
            try
            {
                var video = new Video();

                var insertVideo = () =>
                {
                    video = _mapper.Map<Video>(videoDTO);
                    _context.Videos.InsertOne(video);
                    return true;
                };

                return insertVideo();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<VideoDTO> UpdateVideo(VideoDTO videoDTO)
        {           
            try
            {
                //PEGAR O VIDEO QUE EXISTE NO BANCO
                var video = () =>
                {
                    var getVideoById = _context.Videos.Find<Video>(x => x.Id == videoDTO.Id)
                                    .Project<Video>(Builders<Video>.Projection.Exclude("_id"))
                                    .FirstOrDefault();

                    return _mapper.Map<Video>(getVideoById);
                };

                //VERIFICAR SE EXISTE
                if(video().Id == 0) 
                    return new VideoDTO();

                //ATUALIZAR O VIDEO NO BANCO
                var updateResult = _context.Videos.ReplaceOne(v => v.Id == videoDTO.Id, _mapper.Map<Video>(videoDTO));

                //RETORNAR O VIDEO ATUALIZADO
                if (updateResult.ModifiedCount <= 0)
                    return new VideoDTO();

                return videoDTO;           
            }
            catch(Exception ex)
            {
                return new VideoDTO();
            }
        }

        public async Task<bool> RemoveVideo(long id)
        {
            try
            {
                var videoToRemove = () =>
                {
                    var deleteFilter = Builders<Video>.Filter.Eq(x => x.Id, id);
                    var deleteAttempt = _context.Videos.DeleteOne(deleteFilter);

                    if (deleteAttempt.DeletedCount <= 0)
                        return false;

                    return true;
                };

                return videoToRemove();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                return false;
            }
        }

        public async Task<List<VideoDTO>> GetAndFilterVideosByName(string name)
        {
            try
            {
                var builder = Builders<Video>.Filter;
                var regex = new BsonRegularExpression(name, "i");
                var filter = builder.Regex("title", regex);

                var result = _context.Videos.Find(filter).ToList();

                return _mapper.Map<List<VideoDTO>>(result);
            }
            catch(Exception ex)
            {
                return new List<VideoDTO>();
            }
        }
    }
}
