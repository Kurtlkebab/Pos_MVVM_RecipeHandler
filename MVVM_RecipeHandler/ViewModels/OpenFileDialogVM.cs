using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Prism.Events;
using Microsoft.Win32;
using MVVM_RecipeHandler.Events;
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
    /// Creates OpenFileDialog, builds string from chosen Image file
    /// Derives from <see cref="ViewModelBase"/> class.
    /// </summary>
    public class OpenFileDialogVM : ViewModelBase
    {
        #region ------------- Fields, Constants, Delegates ------------------------
        /// <summary>
        /// Gets or sets Open file Dialog Command
        /// </summary>
        public static RelayCommand OpenCommand { get; set; }

        /// <summary>
        /// Image converted to String
        /// </summary>
        public string base64String;
        
        /// <summary>
        /// the selected filepath of openfiledialog
        /// </summary>
        private string _selectedPath;

        /// <summary>
        /// the initial directory for OpenFileDialog we can specify
        /// </summary>
        private string _defaultPath;
        #endregion-------------------------------------------------------------------

        #region ------------- Constructor, Destructor, Dispose, Clone -------------

        /// <summary>
        /// Gets or sets the Base64string built from image
        /// </summary>
        public string Base64String {get {return base64String ;} set { this.OnPropertyChanged(nameof(this.Base64String)); ; } }

        /// <summary>
        /// Gets or sets the SelectedPatch from Open file Dialog
        /// </summary>
        public string SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                _selectedPath = value;
              
                this.OnPropertyChanged(nameof(this.SelectedPath));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileDialogVM"/> class.
        /// </summary>
        /// /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public OpenFileDialogVM(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            RegisterCommands();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileDialogVM"/> class.
        /// </summary>
        /// <param name="defaultPath"> the initial directory for OpenfileDialgog</param>
        /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public OpenFileDialogVM(IEventAggregator eventAggregator,string defaultPath) : base(eventAggregator)
        {
            _defaultPath = defaultPath;
            RegisterCommands();
        }

        #endregion-------------------------------------------------------------------

        #region ------------- Private helper --------------------------------------
        
        private void PublishImgString()
        {
            try
            {
                Image img = Image.FromFile(_selectedPath);
                string ImageString = ImageToBase64String(img, ImageFormat.Jpeg);
                EventAggregator.GetEvent<ImageStringDataChangedEvent>().Publish(ImageString);
                base64String = ImageString;
                this.OnPropertyChanged(nameof(this.Base64String));
            }
            catch(Exception ex)
            {

            }
          
        }

        /// <summary>
        /// Instantiate new RelayCommand
        /// </summary>
        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
        }

        /// <summary>
        /// creates new OpenFileDialog and sets selectedPatch
        /// </summary>
        private void ExecuteOpenFileDialog()
        {
            var dialog = new OpenFileDialog { InitialDirectory = _defaultPath };
            dialog.Filter= "Image Files|*.jpg;*.jpeg";
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
            PublishImgString();
        }

        /// <summary>
        /// Converts an image to string
        /// </summary>
        /// <param name="image"> image to convert to string</param>
        /// <param name="format"> defines the dataformat of the image</param>
        /// <returns>Image as a string</returns>
        public string ImageToBase64String(Image image, ImageFormat format)
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
        /// <param name="base64"> string to convert to image/param>
        /// <returns> Image built from base64 string</returns>
        public Image ImageFromBase64String(string base64)
        {
            MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64));
            Image result = Image.FromStream(memory);
            memory.Close();

            return result;
        }
        #endregion-------------------------------------------------------------------

    }
}
