using MVVM_RecipeHandler_Models.DataClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_RecipeHandler_EF6._0
{
    /// <summary>
    /// Derives from the <see cref="DbContext"/> class.
    /// </summary>
    public class RecipeContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RecipeContext"/> class
        /// </summary>
        public RecipeContext():base("RecipeDatabaseFinal1")
        {

        }
        public DbSet<Recipe> RecipesSet { get; set; }
        public DbSet<Ingredient> IngredientsSet { get; set; }
        public DbSet<Unit> UnitsSet { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException(
                "Entity Validation Failed - errors follow:\n" +
                sb.ToString(), ex
                );
            }
        }
    }
}
