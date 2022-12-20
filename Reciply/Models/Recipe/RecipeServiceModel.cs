namespace Reciply.Models.Recipe
{
	public class RecipeServiceModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public byte[] RecipeImage { get; set; }
		public string Ingridients { get; set; } 
		public string CookingInstructions { get; set; } 
	}
}
