using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Repository;
using AluraFlixChallenge.API.Responses;

namespace AluraFlixChallenge.API.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _repository;

        public VideoService(IVideoRepository repository)
        {
            _repository= repository;
        }

        public async Task<InternalResponses<VideoDTO>> GetVideos()
        {
            try
            {
                var videosList = await _repository.GetVideos();

                if (videosList.Count < 1)
                    return new InternalResponses<VideoDTO>
                    {
                        Code = 204,
                        Data = new List<VideoDTO>(),
                        Message = "No items found in database",
                        Exception = null
                    };

                return new InternalResponses<VideoDTO>
                {
                    Code = 200,
                    Data = videosList,
                    Message = "OK",
                    Exception = null
                };
            }
            catch(Exception ex)
            {
                return new InternalResponses<VideoDTO>
                {
                    Code = 500,
                    Data = new List<VideoDTO>(),
                    Message = ex.Message,
                    Exception = ex.InnerException.ToString()
                };
            }
        }
    }
}
