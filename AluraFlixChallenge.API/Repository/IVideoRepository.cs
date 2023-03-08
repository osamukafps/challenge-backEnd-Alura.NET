using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Entities;

namespace AluraFlixChallenge.API.Repository
{
    public interface IVideoRepository
    {
        Task<List<VideoDTO>> GetVideos();
        Task<VideoDTO> GetVideoById(long id);
        Task<bool> PostVideo(VideoDTO video);
        Task<VideoDTO> UpdateVideo(VideoDTO videoDTO);
        Task<bool> RemoveVideo(long id);
    }
}
