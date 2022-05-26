using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_RecipeHandler.Events
{
    /// <summary>
    /// Event signalizing a change in image string data.
    /// </summary>
    public class ImageStringDataChangedEvent : CompositePresentationEvent<string>
    {
    }
}
