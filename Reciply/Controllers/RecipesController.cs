namespace Reciply.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Reciply.Contracts;
    using Reciply.Models.Recipe;

    [Authorize]
    public class RecipesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeService _recipeSerice;

        public RecipesController(ILogger<HomeController> logger, IRecipeService recipeSerice)
        {
            _logger = logger;
            _recipeSerice = recipeSerice;
        }

		[HttpGet]
		public IActionResult Index([FromQuery]AllRecipesQueryModel query)
        {
            var recipes = _recipeSerice.All(
                query.SearchTerm,
                query.CurrentPage,
                query.RecipesPerPage);

            query.TotalRecipesCount = recipes.TotalRecipesCount;
            query.Recipes = recipes.Recipes;

            return View(query);
        }

        [HttpGet]
        [Route("/Recipes/RecipeInformation/{recipeId}")]
        public async Task <IActionResult> RecipeInformation(Guid recipeId)
        {
            var recipe = await _recipeSerice.GetRecipeAsync(recipeId);
            return View(recipe);
        }

    }
}
