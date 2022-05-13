namespace MVVM_RecipeHandler_EF6._0.Migrations
{
    using MVVM_RecipeHandler_Models.DataClasses;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVVM_RecipeHandler_EF6._0.RecipeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVVM_RecipeHandler_EF6._0.RecipeContext context)
        {
            if (context.RecipesSet.Any())
            {
                return;
            }


            context.RecipesSet.AddRange(new List<Recipe>)
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
