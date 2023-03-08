using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AluraFlixChallenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService service)
        {
            _videoService= service;
        }

        [HttpGet]
        public async Task<IActionResult> GetVideos()
        {
            try
            {
                var listVideos = await _videoService.GetVideos();

                if (listVideos.Code != 200)
                    return StatusCode(204, listVideos);

                return Ok(listVideos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideoById(long id)
        {
            try
            {
                var video = await _videoService.GetVideoById(id);

                if(video.Code == 204)
                    return StatusCode(204, video);

                return StatusCode(200, video);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostVideo(VideoDTO videoDTO)
        {
            try
            {
                var postVideo = await _videoService.PostVideo(videoDTO);

                if(postVideo.Code != 200)
                    return StatusCode(postVideo.Code, postVideo);

                return StatusCode(200, postVideo);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVideo(VideoDTO videoDTO)
        {
            try
            {
                var updateResult = await _videoService.UpdateVideo(videoDTO);

                if(updateResult.Code != 200)
                    return StatusCode(updateResult.Code, updateResult);

                return StatusCode(200, updateResult);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVideo(long id)
        {
            try
            {
                var deleteAttempt = await _videoService.RemoveVideo(id);

                if (deleteAttempt.Code != 204)
                    return StatusCode(500, deleteAttempt);

                return StatusCode(204, deleteAttempt);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
