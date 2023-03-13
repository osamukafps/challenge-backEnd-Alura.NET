using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Entities;

namespace AluraFlixChallenge.API.Repository
{
    public interface IVideoRepository
    {
        Task<List<VideoDTO>> GetVideos(int pageNumber = 1, int pageSize = 2);
        Task<VideoDTO> GetVideoById(long id);
        Task<bool> PostVideo(VideoDTO video);
        Task<VideoDTO> UpdateVideo(VideoDTO videoDTO);
        Task<bool> RemoveVideo(long id);
        Task<List<VideoDTO>> GetAndFilterVideosByName(string name);
    }
}
