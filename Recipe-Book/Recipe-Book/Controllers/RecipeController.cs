using Recipe_Book.Models;
using Recipe_Book.Services;
using Microsoft.AspNetCore.Mvc;

namespace Recipe_Book.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : Controller
    {
        private readonly RecipeService _recipeService;

        public RecipeController(RecipeService recipeService) => _recipeService = recipeService;

        [HttpGet]
        public async Task<List<Recipe>> Get() => await _recipeService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Recipe>> Get(string id)
        {
            var Recipe = await _recipeService.GetAsync(id);

            if (Recipe == null)
            {
                return NotFound();
            }

            return Recipe;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Recipe newRecipe)
        {
            await _recipeService.CreateAsync(newRecipe);

            return CreatedAtAction(nameof(Get), new { id = newRecipe.Id}, newRecipe);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Recipe updatedRecipe)
        {
            var Recipe = await _recipeService.GetAsync(id);

            if (Recipe is null)
            {
                return NotFound();
            }

            updatedRecipe.Id = id;

            await _recipeService.UpdateAsync(id, updatedRecipe);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Recipe = await _recipeService.GetAsync(id);

            if (Recipe is null)
            {
                return NotFound();
            }

            await _recipeService.RemoveAsync(id);

            return NoContent();
        }
    }
}
