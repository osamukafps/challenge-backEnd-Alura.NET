using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Responses;

namespace AluraFlixChallenge.API.Services
{
    public interface ICategoryService
    {
        Task<InternalResponses> GetAllCategories();
        Task<InternalResponses> GetCategoryById(long id); 
        Task<InternalResponses> PostCategory(CategoryDTO categoryDTO);
        Task<InternalResponses> UpdateCategory(CategoryDTO categoryDTO);
        Task<InternalResponses> RemoveCategory(long id);
        Task<InternalResponses> GetVideosByCategoryId(long categoryId);
    }
}
