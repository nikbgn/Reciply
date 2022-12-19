namespace Reciply.Contracts
{
    using Reciply.Models.Recipe;

    public interface IRecipeService
    {
        /// <summary>
        /// Creates a recipe.
        /// </summary>
        /// <returns></returns>
        
        public Task CreateRecipeAsync(CreateRecipeViewModel model, string userId);

        /// <summary>
        /// Gets all recipes.
        /// </summary>
        /// <returns></returns>
        public Task<AllRecipesQueryModel> GetAllRecipesAsync();
    }
}
