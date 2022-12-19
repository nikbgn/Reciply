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
        /// Creates a recipe.
        /// </summary>
        /// <returns></returns>
        
        public async Task CreateRecipeAsync(CreateRecipeViewModel model, string userId)
        {
            try
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

                    if(ms.Length > 2097152)
                    {
                        _logger.LogError("There was an attempt to upload a file that is too large!");
                        throw new Exception("File is too large!");
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
            catch (Exception ex)
            {
                _logger.LogError("Failed to create recipe!");
            }
            
        }

		public async Task<AllRecipesQueryModel> GetAllRecipesAsync()
		{
            var allRecipes = new AllRecipesQueryModel();

            allRecipes.Recipes = await _context.Recipes.Select(r => new RecipeServiceModel()
            {
                RecipeImage = r.RecipeImage,
                CookingInstructions = r.CookingInstructions,
                Ingridients = r.Ingridients,
                Name = r.Name
            }).ToListAsync();

            return allRecipes;
		}
	}
}
