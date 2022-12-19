namespace Reciply.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public virtual List<RecipeUser> RecipesUsers { get; set; } = new List<RecipeUser>();
    }
}
