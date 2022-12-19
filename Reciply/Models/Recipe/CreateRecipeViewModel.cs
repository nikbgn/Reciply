namespace Reciply.Models.Recipe
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;

    using static Constants.DataValidationConstants;
    using static Constants.DataValidationConstants.RecipeValidationConstants;

    public class CreateRecipeViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = ThisFieldIsRequiredMessage)]
        [StringLength(maximumLength: RecipeNameMaxLength, MinimumLength = RecipeNameMinLength)]
        public string Name { get; set; } = null!;

		[Required(ErrorMessage = ThisFieldIsRequiredMessage)]
        public string Ingridients { get; set; } = null!;

        [Required(ErrorMessage = ThisFieldIsRequiredMessage)]
        public string CookingInstructions { get; set; } = null!;

        [FromForm]
		public IFormFile RecipeImage { get; set; }
	}
}
