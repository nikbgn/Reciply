namespace Reciply.Areas.RecipeManagement.Controllers
{
	using System.Collections;
	using System.Net.Mime;
	using System.Xml.Linq;

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
		[Route("/RecipeManagement/Home/EditRecipe/{recipeId}")]
		public async Task<IActionResult> EditRecipe(Guid recipeId)
		{
			var getRecipe = await _recipeService.GetRecipeAsync(recipeId);
			var recipeToEdit = new CreateRecipeViewModel()
			{
				Id = recipeId,
				CookingInstructions = getRecipe.CookingInstructions,
				Ingridients = getRecipe.Ingridients,
				Name = getRecipe.Name
			};
			return View(recipeToEdit);
		}

		[HttpPost]
		public async Task<IActionResult> EditRecipe(CreateRecipeViewModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(model);
				}
				await _recipeService.EditRecipeAsync(model);
				return RedirectToAction(nameof(MyRecipes));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(model);
			}
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
				return RedirectToAction(nameof(MyRecipes));
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
			return RedirectToAction(nameof(MyRecipes));
		}
	}


}
