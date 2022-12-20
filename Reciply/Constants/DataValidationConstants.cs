namespace Reciply.Constants
{
    public static class DataValidationConstants
    {
        public const string ThisFieldIsRequiredMessage = "Filling this field is mandatory!";

        /// <summary>
        /// Validation constants for recipes.
        /// </summary>
        public static class RecipeValidationConstants
        {
            public const int RecipeNameMinLength = 10;
            public const int RecipeNameMaxLength = 100;

            public const string RecipeNameLengthError = "The recipe name must be between {2} and {1} symbols.";
        }
    }
}
