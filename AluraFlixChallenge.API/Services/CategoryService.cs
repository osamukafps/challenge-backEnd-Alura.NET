using AluraFlixChallenge.API.Context;
using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Repository;
using AluraFlixChallenge.API.Responses;
using Amazon.Runtime.Internal;

namespace AluraFlixChallenge.API.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository repository)
        {
            _categoryRepository = repository;
        }

        public async Task<InternalResponses> GetAllCategories()
        {
            try
            {
                var categoryList = await _categoryRepository.GetCategories();

                if (categoryList.Count == 0)
                    return new InternalResponses
                    {
                        Code = 204,
                        Data = (object)new CategoryDTO(),
                        Message = "No items found in database",
                        Exception = null
                    };

                return new InternalResponses
                {
                    Code = 200,
                    Data = new { categories = categoryList },
                    Message = "OK",
                    Exception = null
                };
            }
            catch (Exception ex)
            {
                return new InternalResponses
                {
                    Code = 500,
                    Data = (object)new CategoryDTO(),
                    Message = ex.Message,
                    Exception = ex.InnerException.ToString()
                };
            }
        }

        public async Task<InternalResponses> GetCategoryById(long id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryById(id);

                if (category.Id == 0)
                    return new InternalResponses
                    {
                        Code = 204,
                        Data = (object)new CategoryDTO(),
                        Message = "Category not found in database",
                        Exception = null
                    };

                return new InternalResponses
                {
                    Code = 200,
                    Data = new { category = category },
                    Message = "OK",
                    Exception = null
                };
            }
            catch (Exception ex)
            {
                return new InternalResponses
                {
                    Code = 500,
                    Data = (object)new CategoryDTO(),
                    Message = ex.Message,
                    Exception = ex.InnerException.ToString()
                };
            }
        }

        public async Task<InternalResponses> PostCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var postAttempt = await _categoryRepository.PostCategory(categoryDTO);

                if (!postAttempt)
                    return new InternalResponses
                    {
                        Code = 500,
                        Data = (object)new CategoryDTO(),
                        Message = "Error inserting data into database",
                        Exception = null
                    };

                return new InternalResponses
                {
                    Code = 201,
                    Data = new { category = categoryDTO },
                    Message = "OK",
                    Exception = null
                };
            }
            catch (Exception ex)
            {
                return new InternalResponses
                {
                    Code = 500,
                    Data = (object)new CategoryDTO(),
                    Message = ex.Message,
                    Exception = ex.InnerException.ToString()
                };
            }
        }

        public async Task<InternalResponses> UpdateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var updateAttempt = await _categoryRepository.UpdateCategory(categoryDTO);

                if (updateAttempt.Id <= 0)
                    return new InternalResponses
                    {
                        Code = 500,
                        Data = (object)new CategoryDTO(),
                        Message = "Data has not been updated",
                        Exception = null
                    };

                return new InternalResponses
                {
                    Code = 200,
                    Data = new { category = categoryDTO },
                    Message = "OK",
                    Exception = null
                };
            }
            catch (Exception ex)
            {
                return new InternalResponses
                {
                    Code = 500,
                    Data = (object)new CategoryDTO(),
                    Message = ex.Message,
                    Exception = ex.InnerException.ToString()
                };
            }
        }

        public async Task<InternalResponses> RemoveCategory(long id)
        {
            try
            {
                var removeAttempt = await _categoryRepository.RemoveCategory(id);

                if (!removeAttempt)
                    return new InternalResponses
                    {
                        Code = 500,
                        Data = new { deleted = false },
                        Message = "An error occurred during deletion",
                        Exception = null
                    };

                return new InternalResponses
                {
                    Code = 200,
                    Data = new { deleted = true },
                    Message = "OK",
                    Exception = null
                };
            }
            catch (Exception ex)
            {
                return new InternalResponses
                {
                    Code = 500,
                    Data = new { deleted = false },
                    Message = ex.Message,
                    Exception = ex.InnerException.ToString()
                };
            }
        }

        public async Task<InternalResponses> GetVideosByCategoryId(long categoryId)
        {
            try
            {
                var videosByCategory = await _categoryRepository.GetVideosByCategoryId(categoryId);

                if (videosByCategory.Count <= 0)
                    return new InternalResponses
                    {
                        Code = 204,
                        Data = (object)new VideoDTO(),
                        Message = "No videos in this category",
                        Exception = null
                    };

                return new InternalResponses
                {
                    Code = 200,
                    Data = new { videos = videosByCategory },
                    Message = "OK",
                    Exception = null
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new InternalResponses
                {
                    Code = 500,
                    Data = (object)new VideoDTO(),
                    Message = ex.Message,
                    Exception = ex.InnerException.ToString()
                };
            }
        }
    }
}

