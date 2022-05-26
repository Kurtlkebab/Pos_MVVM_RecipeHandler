using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler_Common.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        /// <summary>
        /// Converts an image to string
        /// </summary>
        /// <param name="image">image to convert to string</param>
        /// <param name="format">defines the data format of the image</param>
        /// <returns>Image as a string</returns>
        private string ImageToBase64String(Image image, ImageFormat format)
        {
            MemoryStream memory = new MemoryStream();
            image.Save(memory, format);
            string base64 = Convert.ToBase64String(memory.ToArray());
            memory.Close();
            return base64;
        }

        /// <summary>
        /// Builds Image from base64 string
        /// </summary>
        /// <param name="base64">string to convert to image</param>
        /// <returns> Image from string</returns>
        private Image ImageFromBase64String(string base64)
        {
            MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64));
            Image result = Image.FromStream(memory);
            memory.Close();
            return result;
        }

        /// <summary>
        /// Builds and returns an image string created from Path
        /// </summary>
        /// <param name="path"> Path to jpg for string creation</param>
        /// <returns> string Image string built from path</returns>
        private string BuildImgString(string path)
        {
            Image img = Image.FromFile(path);
            string imageString = this.ImageToBase64String(img, ImageFormat.Jpeg);
            return imageString;
        }
        #endregion
    }
}
