using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_RecipeHandler
{
    /// <summary>
    /// Interaktion logic für "App"
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Get references to the the current process
            Process currentProcess = Process.GetCurrentProcess();

            // Check how many total processes have the same name as the current process
            if (Process.GetProcessesByName(currentProcess.ProcessName).Length > 1)
            {
                // If there is more than one, the process is already running
                MessageBox.Show("Application is already running.");
                Application.Current.Shutdown();
                return;
            }

            // Init event aggregator
            IEventAggregator eventAggregator = new EventAggregator();

            // Init View and Viewmodel
            MainWindow mainWindow = new MainWindow();
            MainViewModel mainViewModel = new MainViewModel(eventAggregator);
            mainWindow.DataContext = mainViewModel;

            // Show MainWindow
            mainWindow.Show();
        }
    }
}
