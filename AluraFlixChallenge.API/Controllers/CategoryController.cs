using AluraFlixChallenge.API.Data;
using AluraFlixChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AluraFlixChallenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategories();

                if (categories.Code == 204)
                    return StatusCode(204, categories);

                else if (categories.Code != 200)
                    return StatusCode(500, categories);

                return StatusCode(200, categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(long id)
        {
            try
            {
                var category = await _categoryService.GetCategoryById(id);

                if (category.Code == 204)
                    return StatusCode(204, category);
                else if(category.Code != 200)
                    return StatusCode(500, category);

                return StatusCode(200, category);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("{id}/videos/")]
        public async Task<IActionResult> GetVideosByCategoryId(long id)
        {
            try
            {
                var videosByCategory = await _categoryService.GetVideosByCategoryId(id);

                if (videosByCategory.Code == 204)
                    return StatusCode(204, videosByCategory);

                else if (videosByCategory.Code != 200)
                    return StatusCode(500, videosByCategory);

                return StatusCode(200, videosByCategory);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var postAttempt = await _categoryService.PostCategory(categoryDTO);

                if(postAttempt.Code != 201)
                    return StatusCode(postAttempt.Code, postAttempt);

                return StatusCode(201, postAttempt);
            }
            catch(Exception ex) 
            { 
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var updateAttempt = await _categoryService.UpdateCategory(categoryDTO);

                if (updateAttempt.Code != 200)
                    return StatusCode(updateAttempt.Code, updateAttempt);

                return StatusCode(200, updateAttempt);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCategory(long id)
        {
            try
            {
                var deleteAttempt = await _categoryService.RemoveCategory(id);

                if(deleteAttempt.Code != 200)
                    return StatusCode(deleteAttempt.Code, deleteAttempt);

                return StatusCode(200, deleteAttempt);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
