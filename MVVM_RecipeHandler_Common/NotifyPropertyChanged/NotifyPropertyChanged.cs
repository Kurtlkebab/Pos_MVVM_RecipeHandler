using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_RecipeHandler_Common.NotifyPropertyChanged
{
    /// <summary>
    /// Class for property change notification.
    /// Derives from <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies a listener, that property value has changed.
        /// Name of the property to notify listener.
        /// </summary>
        /// <param name="property">Name of the property.</param>
        public void OnPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
