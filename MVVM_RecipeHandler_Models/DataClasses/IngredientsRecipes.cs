using MVVM_RecipeHandler_Common.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_RecipeHandler_Models.DataClasses
{
    public class IngredientsRecipes:NotifyPropertyChanged
    {
        
            [Key, Column(Order = 1)]
            public int RecipeID { get; set; }
            [Key, Column(Order = 2)]
            public int IngredientID { get; set; }
            public Recipe Recipe { get; set; }
            public Ingredient Ingredient { get; set; }
        
    }
}
