namespace Reciply.Models.Recipe
{
	using System.ComponentModel.DataAnnotations;

	public class AllRecipesQueryModel
	{
		public int RecipesPerPage { get; set; } = 6;
		[Display(Name = "Search")]
		public string SearchTerm { get; set; }
		public int TotalRecipesCount { get; set; }
		public int CurrentPage { get; set; } = 1;
		public IEnumerable<RecipeServiceModel> Recipes { get; set; }
	}
}
