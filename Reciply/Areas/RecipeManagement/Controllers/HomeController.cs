namespace Reciply.Areas.RecipeManagement.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	using Reciply.Contracts;
	using Reciply.Extensions;
	using Reciply.Models.Recipe;

	[Authorize]
	[Area("RecipeManagement")]
	public class HomeController : Controller
	{

		private readonly IRecipeService _recipeService;

		public HomeController(IRecipeService recipeService)
		{
			_recipeService = recipeService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> MyRecipes()
		{
			var userId = User.Id();
			var recipes = await _recipeService.GetMyRecipesAsync(userId);
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
			try
			{
				if (!ModelState.IsValid)
				{
					return View(model);
				}

				var userId = User.Id();
				await _recipeService.CreateRecipeAsync(model, userId);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(model);
			}

		}

		[Route("/RecipeManagement/Home/Delete/{recipeId}")]
		public async Task<IActionResult> Delete(Guid recipeId)
		{
			await _recipeService.DeleteRecipeAsync(recipeId);
			return RedirectToAction(nameof(Index));
		}
	}


}
