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
    }
}
