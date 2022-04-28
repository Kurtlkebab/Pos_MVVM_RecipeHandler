using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler_Common.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_RecipeHandler.ViewModels
{
    /// <summary>
    /// Base class for all ViewModels.
    /// Derives from <see cref="NotifyPropertyChanged"/> class.
    /// </summary>
    public class ViewModelBase : NotifyPropertyChanged
    {
        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public ViewModelBase(IEventAggregator eventAggregator)
        {
            // Init services
            this.EventAggregator = eventAggregator;
        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------
        /// <summary>
        /// Gets access to the event aggregator for communication.
        /// </summary>
        protected IEventAggregator EventAggregator { get; }
        #endregion
    }
}
