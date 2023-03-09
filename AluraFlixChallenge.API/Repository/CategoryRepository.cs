using AluraFlixChallenge.API.Context;
using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Entities;
using AutoMapper;
using MongoDB.Driver;

namespace AluraFlixChallenge.API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MongoDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(MongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetCategories()
        {
            try
            {
                Func<List<CategoryDTO>> getAllCategories = () =>
                {
                    var categoryList = _context.Categories.Find(x => true).ToList();

                    if (categoryList.Count <= 0)
                        return new List<CategoryDTO>();

                    return _mapper.Map<List<CategoryDTO>>(categoryList);
                };

                return getAllCategories();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CategoryDTO>();
            }
        }

        public async Task<CategoryDTO> GetCategoryById(long id)
        {
            try
            {
                var category = _context.Categories.Find<Category>(x => x.Id == id)
                                                  .Project<Category>(Builders<Category>.Projection.Exclude("_id"))
                                                  .FirstOrDefault();

                if (category is null)
                    return new CategoryDTO();

                return _mapper.Map<CategoryDTO>(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new CategoryDTO();
            }
        }

        public async Task<bool> PostCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var postVideo = () =>
                {
                    var category = _mapper.Map<Category>(categoryDTO);
                    _context.Categories.InsertOne(category);

                    var insertValidationFilter = Builders<Category>.Filter.Eq("title", category.Title);
                    var insertValidationResult = _context.Categories.Find(insertValidationFilter).FirstOrDefault();

                    if (insertValidationResult is null)
                        return false;

                    return true;
                };

                return postVideo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<CategoryDTO> UpdateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var category = () =>
                {
                    var getCategoryById = _context.Categories.Find<Category>(c => c.Id == categoryDTO.Id)
                                                             .Project<Category>(Builders<Category>.Projection.Exclude("_id"))
                                                             .FirstOrDefault();

                    return getCategoryById;
                };

                if (category().Id == 0)
                    return new CategoryDTO();

                var updateResult = _context.Categories.ReplaceOne(c => c.Id == categoryDTO.Id, _mapper.Map<Category>(categoryDTO));

                if (updateResult.ModifiedCount <= 0)
                    return new CategoryDTO();

                return categoryDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new CategoryDTO();
            }
        }

        public async Task<bool> RemoveCategory(long id)
        {
            try
            {
                var categoryToRemove = () =>
                {
                    var deleteFilter = Builders<Category>.Filter.Eq(c => c.Id, id);
                    var deleteAttempt = _context.Categories.DeleteOne(deleteFilter);

                    if (deleteAttempt.DeletedCount <= 0)
                        return false;

                    return true;
                };

                return categoryToRemove();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
