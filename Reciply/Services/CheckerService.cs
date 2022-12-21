namespace Reciply.Services
{
	using System;
	using System.Threading.Tasks;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;

	using Reciply.Contracts;
	using Reciply.Data;

	public class CheckerService : ICheckerService
	{

		private readonly ReciplyDbContext _context;
		private readonly ILogger<CheckerService> _logger;

		public CheckerService(ReciplyDbContext context, ILogger<CheckerService> logger)
		{
			_context = context;
			_logger = logger;
		}

		/// <summary>
		/// Checks if an user is the author of a recipe.
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="recipeId"></param>
		/// <returns></returns>
		public async Task<bool> CheckIfUserIsRecipeAuthor(string userId, Guid recipeId)
		{
			try
			{
				var user = await _context
					.Users
					.Include(u => u.RecipesUsers)
					.FirstOrDefaultAsync(u => u.Id == userId);


				if (user == null) throw new ArgumentException("Invalid user ID!");

				bool check = user.RecipesUsers.Any(r => r.RecipeId  == recipeId);
				return check;

			}
			catch (Exception ex)
			{
				_logger.LogError("Something went wrong while checking if an user is the author of a recipe.");
				throw new Exception(ex.Message);
			}
		}
	}
}
