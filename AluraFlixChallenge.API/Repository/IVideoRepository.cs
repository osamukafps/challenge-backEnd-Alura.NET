using AluraFlixChallenge.API.Data;

namespace AluraFlixChallenge.API.Repository
{
    public interface IVideoRepository
    {
        Task<List<VideoDTO>> GetVideos();
    }
}
