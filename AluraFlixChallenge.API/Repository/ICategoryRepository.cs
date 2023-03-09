using AluraFlixChallenge.API.Data;

namespace AluraFlixChallenge.API.Repository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetCategoryById(long id);
        Task<bool> PostCategory(CategoryDTO categoryDTO);
        Task<CategoryDTO> UpdateCategory(CategoryDTO categoryDTO);
        Task<bool> RemoveCategory(long id);
    }
}
