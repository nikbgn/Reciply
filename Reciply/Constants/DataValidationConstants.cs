namespace Reciply.Constants
{
    public static class DataValidationConstants
    {
        public const string ThisFieldIsRequiredMessage = "Попълването на това поле е задължително!";

        /// <summary>
        /// Validation constants for recipes.
        /// </summary>
        public static class RecipeValidationConstants
        {
            public const int RecipeNameMinLength = 10;
            public const int RecipeNameMaxLength = 100;

            public const string RecipeNameLengthError = "Заглавието на рецептата трябва да е между {2} и {1} символа.";
        }
    }
}
