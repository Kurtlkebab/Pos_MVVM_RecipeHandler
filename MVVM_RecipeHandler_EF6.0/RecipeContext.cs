using MVVM_RecipeHandler_Models.DataClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_RecipeHandler_EF6._0
{
    public class RecipeContext : DbContext
    {
        public RecipeContext():base("RecipeDatabase")
        {

        }

        public DbSet<Recipe> RecipesSet { get; set; }
        public DbSet<Ingredient> IngredientsSet { get; set; }
      
      
        public DbSet<Unit> UnitsSet { get; set; }
    }
}
