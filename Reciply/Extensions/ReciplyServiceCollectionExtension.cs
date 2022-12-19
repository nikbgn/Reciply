namespace Microsoft.Extensions.DependencyInjection
{
    using Reciply.Contracts;
    using Reciply.Services;

    public static class ReciplyServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRecipeService, RecipeService>();

            return services;
        }
    }
}
