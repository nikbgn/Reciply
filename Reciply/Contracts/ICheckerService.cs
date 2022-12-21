namespace Reciply.Contracts
{
	public interface ICheckerService
	{
		/// <summary>
		/// Checks if an user is the author of a recipe.
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="recipeId"></param>
		/// <returns></returns>
		
		public Task<bool> CheckIfUserIsRecipeAuthor(string userId, Guid recipeId);
	}
}
