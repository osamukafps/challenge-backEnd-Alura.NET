using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Responses;

namespace AluraFlixChallenge.API.Services
{
    public interface IVideoService
    {
        Task<InternalResponses<VideoDTO>> GetVideos();
    }
}
