namespace Reciply.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Reciply.Data.Models;

    public class ReciplyDbContext : IdentityDbContext<User>
    {
        public ReciplyDbContext(DbContextOptions<ReciplyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RecipeUser>()
            .HasKey(x => new { x.UserId, x.RecipeId });

            base.OnModelCreating(builder);
        }
    }
}