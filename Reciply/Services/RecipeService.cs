namespace Reciply.Services
{
	using System.Threading.Tasks;

	using Microsoft.EntityFrameworkCore;

	using Reciply.Contracts;
	using Reciply.Data;
	using Reciply.Data.Models;
	using Reciply.Models.Recipe;

	public class RecipeService : IRecipeService
	{
		private readonly ReciplyDbContext _context;
		private readonly ILogger<RecipeService> _logger;

		public RecipeService(ReciplyDbContext context, ILogger<RecipeService> logger)
		{
			_context = context;
			_logger = logger;
		}

		/// <summary>
		/// Gets all recipes
		/// </summary>
		/// <param name="searchTerm"></param>
		/// <param name="currentPage"></param>
		/// <param name="recipesPerPage"></param>
		/// <returns></returns>

		public RecipeQueryServiceModel All(string searchTerm = null, int currentPage = 1, int recipesPerPage = 1)
		{
			var recipesQuery = _context.Recipes.AsQueryable();
			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				recipesQuery = recipesQuery.Where(r => r.Name.ToLower().Contains(searchTerm.ToLower()));
			}

			var recipes = recipesQuery
				.Skip((currentPage - 1) * recipesPerPage)
				.Take(recipesPerPage)
				.Select(r => new RecipeServiceModel
				{
					Id = r.Id,
					Name = r.Name,
					RecipeImage = r.RecipeImage,
					Ingridients = r.Ingridients,
					CookingInstructions = r.CookingInstructions
				})
				.ToList();

			var totalRecipes = recipesQuery.Count();

			return new RecipeQueryServiceModel()
			{
				TotalRecipesCount = totalRecipes,
				Recipes = recipes
			};

		}

		/// <summary>
		/// Creates a recipe.
		/// </summary>
		/// <returns></returns>

		public async Task CreateRecipeAsync(CreateRecipeViewModel model, string userId)
		{

			var author =
			await _context.Users
			.Include(u => u.RecipesUsers)
			.FirstOrDefaultAsync(u => u.Id == userId);

			if (author == null)
			{
				_logger.LogError("Invalid user id!");
				throw new ArgumentException("Invalid user id!");
			}

			var newRecipe = new Recipe()
			{
				Name = model.Name,
				CookingInstructions = model.CookingInstructions,
				Ingridients = model.Ingridients
			};

			if (model.RecipeImage != null)
			{

				string[] acceptedExtensions = { ".png", ".jpg", ".jpeg" };

				if (!acceptedExtensions.Contains(Path.GetExtension(model.RecipeImage.FileName)))
				{
					_logger.LogError("Invalid image format!");
					throw new FormatException();
				}

				using MemoryStream ms = new MemoryStream();
				await model.RecipeImage.CopyToAsync(ms);

				if (ms.Length > 2097152)
				{
					_logger.LogError("There was an attempt to upload a file that is too large!");
					throw new Exception("The size of the image you tried to upload is too large!");
				}

				newRecipe.RecipeImage = ms.ToArray();
			}
			else
			{
				_logger.LogError("No image found!...");
				throw new Exception("No image found!...");
			}

			author.RecipesUsers.Add(new RecipeUser()
			{
				User = author,
				UserId = author.Id,
				Recipe = newRecipe,
				RecipeId = newRecipe.Id
			});

			await _context.SaveChangesAsync();



		}

		/// <summary>
		/// Deletes a recipe from the database.
		/// </summary>
		/// <param name="recipeId"></param>
		/// <returns></returns>

		public async Task DeleteRecipeAsync(Guid recipeId)
		{
			try
			{
				var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
				if (recipe == null)
				{
					_logger.LogError("Invalid recipe id!");
					throw new ArgumentException();
				}

				_context.Recipes.Remove(recipe);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError($"There was a problem with deleting the recipe... {ex.Message}");
			}
		}



		/// <summary>
		/// Edits recipe
		/// </summary>
		/// <param name="recipeId"></param>
		/// <returns></returns>

		public async Task EditRecipeAsync(CreateRecipeViewModel model)
		{
			var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == model.Id);
			if(recipe == null)
			{
				_logger.LogError("The given recipe id was invalid.");
				throw new ArgumentException("The given recipe id was invalid.");
			}

			try
			{
				recipe.Id = model.Id;
				recipe.Name = model.Name;
				recipe.Ingridients = model.Ingridients;
				recipe.CookingInstructions = model.CookingInstructions;
				if (model.RecipeImage != null)
				{

					string[] acceptedExtensions = { ".png", ".jpg", ".jpeg" };

					if (!acceptedExtensions.Contains(Path.GetExtension(model.RecipeImage.FileName)))
					{
						_logger.LogError("Invalid image format!");
						throw new FormatException();
					}

					using MemoryStream ms = new MemoryStream();
					await model.RecipeImage.CopyToAsync(ms);

					if (ms.Length > 2097152)
					{
						_logger.LogError("There was an attempt to upload a file that is too large!");
						throw new Exception("The size of the image you tried to upload is too large!");
					}

					recipe.RecipeImage = ms.ToArray();
				}
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError("Something went wrong while editing the recipe.");
				throw new ApplicationException(ex.Message);
			}
		}


		/// <summary>
		/// Gets the recipes created from an user.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>

		public async Task<IEnumerable<RecipeServiceModel>> GetMyRecipesAsync(string userId)
		{
			var userRecipes = await _context.Users
				.Where(u => u.Id == userId)
				.Include(u => u.RecipesUsers)
				.ThenInclude(r => r.Recipe)
				.FirstOrDefaultAsync();

			if (userRecipes == null)
			{
				_logger.LogError("The given user id was invalid.");
				throw new ArgumentException("Invalid user id!");
			}

			try
			{
				return userRecipes.RecipesUsers
					.Select(r => new RecipeServiceModel
					{
						Id = r.RecipeId,
						RecipeImage = r.Recipe.RecipeImage,
						CookingInstructions = r.Recipe.CookingInstructions,
						Ingridients = r.Recipe.Ingridients,
						Name = r.Recipe.Name
					}).ToList();
			}
			catch (Exception)
			{
				_logger.LogError("Something went wrong in getting user's recipes.");
				throw new ApplicationException("Something went wrong in getting user's recipes.");
			}
		}


		/// <summary>
		/// Gets a recipe by id.
		/// </summary>
		/// <param name="recipeId"></param>
		/// <returns></returns>

		public async Task<RecipeServiceModel> GetRecipeAsync(Guid recipeId)
		{
			var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
			if (recipe == null)
			{
				_logger.LogError("The given recipe id was invalid.");
				throw new ArgumentException("The given recipe id was invalid.");
			}

			return new RecipeServiceModel()
			{
				Id = recipe.Id,
				Name = recipe.Name,
				Ingridients = recipe.Ingridients,
				CookingInstructions = recipe.CookingInstructions,
				RecipeImage = recipe.RecipeImage
			};
		}
	}
}
