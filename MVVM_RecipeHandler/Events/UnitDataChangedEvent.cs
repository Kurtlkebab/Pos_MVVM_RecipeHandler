using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler_Models.DataClasses;

namespace MVVM_RecipeHandler.Events
{
    /// <summary>
    /// Event signalizing a change in unit data.
    /// </summary>
    public class UnitDataChangedEvent : CompositePresentationEvent<Unit>
    {
    }
}
