namespace Reciply.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using static Constants.DataValidationConstants;
    using static Constants.DataValidationConstants.RecipeValidationConstants;

    public class Recipe
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = ThisFieldIsRequiredMessage)]
        [StringLength(maximumLength: RecipeNameMaxLength, MinimumLength = RecipeNameMinLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = ThisFieldIsRequiredMessage)]
        public byte[] RecipeImage { get; set; } = null!;

        [Required(ErrorMessage = ThisFieldIsRequiredMessage)]
        public string Ingridients { get; set; } = null!;

        [Required(ErrorMessage = ThisFieldIsRequiredMessage)]
        public string CookingInstructions { get; set; } = null!;

        public virtual List<RecipeUser> RecipesUsers { get; set; } = new List<RecipeUser>();
    }
}
