namespace Reciply.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using Reciply.Contracts;
    using Reciply.Models;
    using Reciply.Models.Recipe;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeService _recipeService;

        public HomeController(ILogger<HomeController> logger, IRecipeService recipeService)
        {
            _logger = logger;
            _recipeService = recipeService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var recipes = await _recipeService.GetRecipesForFirstPage();
                return View(recipes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var empty = new RecipeServiceModel[0];
                return View(empty);
            }
        }

        public IActionResult About()
        {
            return View();
        }

    }
}