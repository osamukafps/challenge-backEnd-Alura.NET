using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Repository;
using AluraFlixChallenge.API.Responses;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AluraFlixChallenge.API.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _repository;

        public VideoService(IVideoRepository repository)
        {
            _repository= repository;
        }

        public async Task<InternalResponses> GetVideos()
        {
            try
            {
                var videosList = await _repository.GetVideos();

                if (videosList.Count < 1)
                    return new InternalResponses
                    {
                        Code = 204,
                        Data = (object) new VideoDTO(),
                        Message = "No items found in database",
                        Exception = null
                    };

                return new InternalResponses
                {
                    Code = 200,
                    Data = new { videos = videosList },
                    Message = "OK",
                    Exception = null
                };
            }
            catch(Exception ex)
            {
                return new InternalResponses
                {
                    Code = 500,
                    Data = JsonSerializer.Serialize<List<VideoDTO>>(new List<VideoDTO>()),
                    Message = ex.Message,
                    Exception = ex.InnerException.ToString()
                };
            }
        }

        public async Task<InternalResponses> GetVideoById(long id)
        {
            try
            {
                var video = await _repository.GetVideoById(id);

                if (video.Id == 0)
                    return new InternalResponses
                    {
                        Code = 204,
                        Data = (object) new VideoDTO(),
                        Message = "No items found in database",
                        Exception = null
                    };

                return new InternalResponses
                {
                    Code = 200,
                    Data = new { video = video },
                    Message = "OK",
                    Exception = null
                };
            }
            catch(Exception ex)
            {
                return new InternalResponses
                {
                    Code = 500,
                    Data = (object)new VideoDTO(),
                    Message = ex.Message,
                    Exception = ex.InnerException.ToString()
                };
            }
        }

        public async Task<InternalResponses> PostVideo(VideoDTO videoDTO)
        {
            try
            {
                var validateData = ValidateModel(videoDTO);

                if (!validateData.Item1)
                {
                    string errors = string.Join(",", validateData.Item2); 
                    return new InternalResponses
                    {
                        Code = 500,
                        Data = (object)new VideoDTO(),
                        Message = errors,
                        Exception = null
                    };
                }                  

                var postVideo = await _repository.PostVideo(videoDTO);

                if (postVideo is false)
                    return new InternalResponses
                    {
                        Code = 500,
                        Data = (object)new VideoDTO(),
                        Message = "Error inserting data into database",
                        Exception = null
                    };

                return new InternalResponses
                {
                    Code = 200,
                    Data = (object)videoDTO,
                    Message = "OK",
                    Exception = null
                };
            }
            catch(Exception ex)
            {
                return new InternalResponses
                {
                    Code = 500,
                    Data = (object)new VideoDTO(),
                    Message = ex.Message,
                    Exception = ex.InnerException.ToString()
                };
            }
        }

        public async Task<InternalResponses> UpdateVideo(VideoDTO videoDTO)
        {
            try
            {
                var updateVideo = await _repository.UpdateVideo(videoDTO);

                if (updateVideo.Id <= 0)
                    return new InternalResponses
                    {
                        Code = 500,
                        Data = (object)new VideoDTO(),
                        Message = "Data has not been updated",
                        Exception = null
                    };

                return new InternalResponses
                {
                    Code = 200,
                    Data = (object)videoDTO,
                    Message = "OK",
                    Exception = null
                };
            }
            catch(Exception ex)
            {
                return new InternalResponses
                {
                    Code = 500,
                    Data = (object)new VideoDTO(),
                    Message = ex.Message,
                    Exception = ex.ToString()
                };
            }
        }

        public async Task<InternalResponses> RemoveVideo(long id)
        {
            try
            {
                var deleteVideo = await _repository.RemoveVideo(id);

                if (!deleteVideo)
                    return new InternalResponses
                    {
                        Code = 500,
                        Data = (object)deleteVideo,
                        Message = "An error occurred during deletion",
                        Exception = null
                    };

                return new InternalResponses
                {
                    Code = 204,
                    Data = (object)deleteVideo,
                    Message = "OK",
                    Exception = null
                };
            }
            catch(Exception ex)
            {
                return new InternalResponses
                {
                    Code = 500,
                    Data = (object)ex,
                    Message = ex.Message,
                    Exception = ex.InnerException.ToString()
                };
            }
        }

        #region Private Methods

        private (bool, List<string>) ValidateModel(VideoDTO videoDTO)
        {
            var validateResult = new List<ValidationResult>();
            var context = new ValidationContext(videoDTO, null, null);

            Validator.TryValidateObject(videoDTO, context, validateResult, true);

            if(validateResult.Count > 0)
            {
                List<string> errors = new();

                foreach(var result in validateResult)
                {
                    errors.Add(result.ErrorMessage);
                    Console.WriteLine(result.ErrorMessage);
                }
                
                return (false, errors);
            }

            return (true, new List<string>());
        }
        #endregion
    }
}
