using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Responses;

namespace AluraFlixChallenge.API.Services
{
    public interface IVideoService
    {
        Task<InternalResponses> GetVideos();
        Task<InternalResponses> GetVideoById(long id);
        Task<InternalResponses> PostVideo(VideoDTO videoDTO);
        Task<InternalResponses> UpdateVideo(VideoDTO videoDTO);
        Task<InternalResponses> RemoveVideo(long id);
    }
}
