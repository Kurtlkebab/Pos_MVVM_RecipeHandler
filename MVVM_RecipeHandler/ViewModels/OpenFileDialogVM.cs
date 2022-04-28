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
    public class OpenFileDialogVM : ViewModelBase
    {
        public static RelayCommand OpenCommand { get; set; }
        public string base64String;
        private string _selectedPath;
        public string Base64String { get; set; }
        public string SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                _selectedPath = value;
                Image img = Image.FromFile(value);
                string ImageString = ImageToBase64String(img, ImageFormat.Jpeg);
                EventAggregator.GetEvent<ImageStringDataChangedEvent>().Publish(ImageString);
                this.OnPropertyChanged(nameof(this.Base64String));
                this.OnPropertyChanged(nameof(this.SelectedPath));
            }
        }

        private string _defaultPath;

        public OpenFileDialogVM(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            RegisterCommands();
        }

        public OpenFileDialogVM(IEventAggregator eventAggregator,string defaultPath) : base(eventAggregator)
        {
            _defaultPath = defaultPath;
            RegisterCommands();
        }

        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
        }

        private void ExecuteOpenFileDialog()
        {
            var dialog = new OpenFileDialog { InitialDirectory = _defaultPath };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
        }
        public string ImageToBase64String(Image image, ImageFormat format)
        {
            MemoryStream memory = new MemoryStream();
            image.Save(memory, format);
            string base64 = Convert.ToBase64String(memory.ToArray());
            memory.Close();

            return base64;
        }

        public Image ImageFromBase64String(string base64)
        {
            MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64));
            Image result = Image.FromStream(memory);
            memory.Close();

            return result;
        }
    }
}
