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
		/// Gets all recipes
		/// </summary>
		/// <param name="searchTerm"></param>
		/// <param name="currentPage"></param>
		/// <param name="recipesPerPage"></param>
		/// <returns></returns>

		public RecipeQueryServiceModel All(
            string searchTerm = null,
            int currentPage = 1,
            int recipesPerPage = 1);

        /// <summary>
        /// Deletes a recipe from the database.
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        
        public Task DeleteRecipeAsync(Guid recipeId);

        /// <summary>
        /// Gets the recipes created from an user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        
        public Task<IEnumerable<RecipeServiceModel>> GetMyRecipesAsync(string userId);

    }
}
