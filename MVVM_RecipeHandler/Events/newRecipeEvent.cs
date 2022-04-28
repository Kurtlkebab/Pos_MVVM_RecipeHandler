using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler_Models.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_RecipeHandler.Events
{
    /// <summary>
    /// Event signalizing a change in selected student data.
    /// </summary>
    public class newRecipeEvent : CompositePresentationEvent<Recipe>
    {
    }
}
