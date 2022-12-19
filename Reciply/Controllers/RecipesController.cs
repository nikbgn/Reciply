namespace Reciply.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Reciply.Contracts;
    using Reciply.Extensions;
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

        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeSerice.GetAllRecipesAsync();
            return View(recipes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateRecipeViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeViewModel model)
        {
            var userId = User.Id();
            await _recipeSerice.CreateRecipeAsync(model, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
