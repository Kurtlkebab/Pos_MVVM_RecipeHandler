using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Prism.Events;
using Microsoft.Win32;
using MVVM_RecipeHandler.Events;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MVVM_RecipeHandler.ViewModels
{
    /// <summary>
    /// Creates OpenFileDialog, builds string from chosen Image file
    /// Derives from <see cref="ViewModelBase"/> class.
    /// </summary>
    public class OpenFileDialogVM : ViewModelBase
    {
        #region ------------- Fields, Constants, Delegates ------------------------
       
        /// <summary>
        /// Image converted to String
        /// </summary>
        private string base64String;
        
        /// <summary>
        /// the selected file path of open file dialog
        /// </summary>
        private string selectedPath;

        /// <summary>
        /// the initial directory for Open File Dialog we can specify
        /// </summary>
        private string defaultPath;
        #endregion-------------------------------------------------------------------

        #region ------------- Constructor, Destructor, Dispose, Clone -------------

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileDialogVM"/> class.
        /// </summary>
        /// /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public OpenFileDialogVM(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.RegisterCommands();
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="OpenFileDialogVM"/> class.
        /// </summary>
        /// <param name="eventAggregator">the initial directory for open file dialog</param>
        /// <param name="defaultPath">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public OpenFileDialogVM(IEventAggregator eventAggregator, string defaultPath) : base(eventAggregator)
        {
            this.defaultPath = defaultPath;
            this.RegisterCommands();
        }

        #endregion-------------------------------------------------------------------

        #region ------------- Properties, Indexer ---------------------------------
        /// <summary>
        /// Gets or sets Open file Dialog Command
        /// </summary>
        public static RelayCommand OpenCommand { get; set; }
       
        /// <summary>
        /// Gets or sets the Base64string built from image
        /// </summary>
        public string Base64String
        {
            get
            {
                return this.base64String;
            }
            
            set
            {
                this.OnPropertyChanged(nameof(this.Base64String)); 
            }
        }

        /// <summary>
        /// Gets or sets the SelectedPatch from Open file Dialog
        /// </summary>
        public string SelectedPath
        {
            get
            {
                return this.selectedPath;
            }
            
            set
            {
                this.selectedPath = value;
                this.OnPropertyChanged(nameof(this.SelectedPath));
            }
        }
        #endregion -------------------------------------------------------------

        #region ------------- Private helper --------------------------------------
        /// <summary>
        /// Fires <see cref="ImageStringDataChangedEvent"/> event with image string
        /// </summary>
        private void PublishImgString()
        {
            try
            {
                Image img = Image.FromFile(this.selectedPath);
                string imageString = this.ImageToBase64String(img, ImageFormat.Jpeg);
                EventAggregator.GetEvent<ImageStringDataChangedEvent>().Publish(imageString);
                this.base64String = imageString;
                this.OnPropertyChanged(nameof(this.Base64String));
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Instantiate new RelayCommand
        /// </summary>
        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(this.ExecuteOpenFileDialog);
        }

        /// <summary>
        /// creates new OpenFileDialog and sets selected Path and calls Publish Image String to Publish the Image string with selected file path
        /// </summary>
        private void ExecuteOpenFileDialog()
        {
            var dialog = new OpenFileDialog { InitialDirectory = this.defaultPath };
            dialog.Filter = "Image Files|*.jpg;*.jpeg";
            dialog.ShowDialog();
            this.SelectedPath = dialog.FileName;
            this.PublishImgString();
        }

        /// <summary>
        /// Converts an image to string
        /// </summary>
        /// <param name="image"> image to convert to string</param>
        /// <param name="format"> defines the data format of the image</param>
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
        /// Converts a string to image
        /// </summary>
        /// <param name="base64">string to convert to image</param>
        /// <returns>Image built from base64 string</returns>
        private Image ImageFromBase64String(string base64)
        {
            MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64));
            Image result = Image.FromStream(memory);
            memory.Close();

            return result;
        }
        #endregion-------------------------------------------------------------------
    }
}
