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
                    return StatusCode(404, listVideos);

                return Ok(listVideos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
