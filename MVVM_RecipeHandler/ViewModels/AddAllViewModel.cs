using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.Views;
using MVVM_RecipeHandler_Common.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM_RecipeHandler.ViewModels
{
    /// <summary>
    /// Main ViewModel of the application.
    /// Derives from the <see cref="ViewModelBase"/> class.
    /// </summary>
    public class AddAllViewModel : ViewModelBase
    {
        #region ------------- Fields, Constants, Delegates ------------------------
        private AddIngredientsView memoryView;
        /// <summary>
        /// View that is currently bound to the left ContentControl.
        /// </summary>
        private UserControl currentViewLeft;

        /// <summary>
        /// View that is currently bound to the left ContentControl.
        /// </summary>
        private UserControl currentViewLeftRight;

        /// <summary>
        /// View that is currently bound to the rightContentControl.
        /// </summary>
        private UserControl currentViewRight;

        /// <summary>
        /// View that is currently bound to the bottom ContentControl.
        /// </summary>
        private UserControl currentViewBottom;
        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public AddAllViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {



            AddUnitsView cView = new AddUnitsView();
            UnitAdderViewModel cVm = new UnitAdderViewModel(EventAggregator);
            cView.DataContext = cVm;


            this.CurrentViewLeft = cView;



            AddIngredientsView addIngredientsView = new AddIngredientsView();
            IngredientAdderViewModel vm = new IngredientAdderViewModel(EventAggregator);
            addIngredientsView.DataContext = vm;
            this.CurrentViewLeftRight = addIngredientsView;


            AddRecipeView addRView = new AddRecipeView();
            AddRecipeViewModel Advm = new AddRecipeViewModel(EventAggregator);

            addRView.DataContext = Advm;
            this.CurrentViewRight = addRView;

            // hookup command to assoiated methode
            this.AddIngredientViewCommand = new ActionCommand(this.AddIngredientViewCommandExecute, this.AddIngredientViewCommandCanExecute);
            this.AddRecipeViewCommand = new ActionCommand(this.AddRecipeViewCommandExecute, this.AddRecipeViewCommandCanExecute);
            this.AddInspectCurrentRecipeViewCommand = new ActionCommand(this.AddInspectCurrentRecipeViewCommandExecute, this.AddInspectCurrentRecipeViewCommandCanExecute);
            this.AddMainButtonViewCommand = new ActionCommand(this.AddMainButtonViewCommandExecute, this.AddMainButtonViewCommandCanExecute);
            this.AddCartViewCommand = new ActionCommand(this.AddCartViewCommandExecute, this.AddCartViewCommandCanExecute);
            this.AddUnitsViewCommand = new ActionCommand(this.AddUnitsViewCommandExecute, this.AddUnitsViewCommandCanExecute);
            this.AddFileDialogViewCommand = new ActionCommand(this.AddFileDialogViewCommandExecute, this.AddFileDialogViewCommandCanExecute);

        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------
        /// <summary>
        /// Gets the student view loading button command.
        /// </summary>
        public ICommand AddIngredientViewCommand { get; private set; }

        /// <summary>
        /// Gets the student view loading button command.
        /// </summary>
        public ICommand AddCartViewCommand { get; private set; }

        /// <summary>
        /// Gets the student view loading button command.
        /// </summary>
        public ICommand AddRecipeViewCommand { get; private set; }

        /// <summary>
        /// Gets the student view loading button command.
        /// </summary>
        public ICommand AddInspectCurrentRecipeViewCommand { get; private set; }

        /// <summary>
        /// Gets the student view loading button command.
        /// </summary>
        public ICommand AddMainButtonViewCommand { get; private set; }

        /// <summary>
        /// Gets the student view loading button command.
        /// </summary>
        public ICommand AddUnitsViewCommand { get; private set; }

        /// <summary>
        /// Gets the student view loading button command.
        /// </summary>
        public ICommand AddFileDialogViewCommand { get; private set; }

        /// <summary>
        /// Gets the settings view loading button command.
        /// </summary>
        public ICommand SettingsViewCommand { get; private set; }

        /// <summary>
        /// Gets or sets the view that is currently bound to the left ContentControl.
        /// </summary>
        public UserControl CurrentViewLeft
        {
            get
            {
                return this.currentViewLeft;
            }

            set
            {
                if (this.currentViewLeft != value)
                {
                    this.currentViewLeft = value;
                    this.OnPropertyChanged(nameof(this.CurrentViewLeft));
                }
            }
        }

        /// <summary>
        /// Gets or sets the view that is currently bound to the left ContentControl.
        /// </summary>
        public UserControl CurrentViewLeftRight
        {
            get
            {
                return this.currentViewLeftRight;
            }

            set
            {
                if (this.currentViewLeftRight != value)
                {
                    this.currentViewLeftRight = value;
                    this.OnPropertyChanged(nameof(this.CurrentViewLeftRight));
                }
            }
        }

        /// <summary>
        /// Gets or sets the view that is currently bound to the left ContentControl.
        /// </summary>
        public UserControl CurrentViewRight
        {
            get
            {
                return this.currentViewRight;
            }

            set
            {
                if (this.currentViewRight != value)
                {
                    this.currentViewRight = value;
                    this.OnPropertyChanged(nameof(this.CurrentViewRight));
                }
            }
        }
        /// <summary>
        /// Gets or sets the view that is currently bound to the right ContentControl.
        /// </summary>
        public UserControl CurrentViewBottom
        {
            get
            {
                return this.currentViewBottom;
            }

            set
            {
                if (this.currentViewBottom != value)
                {
                    this.currentViewBottom = value;
                    this.OnPropertyChanged(nameof(this.CurrentViewBottom));
                }
            }
        }
        #endregion

        #region ------------- Commands --------------------------------------------
        /// <summary>
        /// Determines, whether the student view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddIngredientViewCommandCanExecute(object parameter)
        {

            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Student View" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddIngredientViewCommandExecute(object parameter)
        {
            if (this.CurrentViewLeft == null)
            {
                AddIngredientsView addIngredientsView = new AddIngredientsView();
                IngredientAdderViewModel vm = new IngredientAdderViewModel(EventAggregator);
                addIngredientsView.DataContext = vm;
                this.CurrentViewLeft = addIngredientsView;



            }
            else
            {
                this.CurrentViewLeft = memoryView;
            }// init new students view and view model



        }

        /// <summary>
        /// Determines, whether the student view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddRecipeViewCommandCanExecute(object parameter)
        {

            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Student View" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddRecipeViewCommandExecute(object parameter)
        {
            if (this.CurrentViewLeft == null)
            {
                AddRecipeView addRView = new AddRecipeView();
                AddRecipeViewModel Advm = new AddRecipeViewModel(EventAggregator);

                addRView.DataContext = Advm;
                this.CurrentViewLeft = addRView;

            }
            else
            {
                this.CurrentViewLeft = memoryView;
            }// init new students view and view model

        }

        /// <summary>
        /// Determines, whether the student view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddInspectCurrentRecipeViewCommandCanExecute(object parameter)
        {

            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Student View" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddInspectCurrentRecipeViewCommandExecute(object parameter)
        {
            if (this.CurrentViewBottom == null)
            {
                InspectCurrentRecipeView iView = new InspectCurrentRecipeView();
                InspectCurrentRecipeViewModel iVm = new InspectCurrentRecipeViewModel(EventAggregator);
                iView.DataContext = iVm;
                this.CurrentViewBottom = iView;

            }
            else
            {
                this.CurrentViewLeft = memoryView;
            }// init new students view and view model

        }

        /// <summary>
        /// Determines, whether the student view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddMainButtonViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Student View" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddMainButtonViewCommandExecute(object parameter)
        {
            if (this.CurrentViewLeft == null)
            {
                MainButtonView mView = new MainButtonView();
                MainButtonViewModel mVm = new MainButtonViewModel(EventAggregator);
                mView.DataContext = mVm;
                mView.DataContext = mVm;
                this.CurrentViewLeft = mView;
            }
            else
            {
                this.CurrentViewLeft = memoryView;
            }// init new students view and view model

        }


        /// <summary>
        /// Determines, whether the student view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddCartViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Student View" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddCartViewCommandExecute(object parameter)
        {
            if (this.CurrentViewLeft == null)
            {
                ShoppingCartView cView = new ShoppingCartView();
                ShoppingCartViewModel cVm = new ShoppingCartViewModel(EventAggregator);
                cView.DataContext = cVm;
                this.CurrentViewRight = cView;
            }
            else
            {
                this.CurrentViewLeft = memoryView;
            }// init new students view and view model

        }
        /// <summary>
        /// Determines, whether the student view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddUnitsViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Student View" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddUnitsViewCommandExecute(object parameter)
        {
            if (this.CurrentViewLeft == null)
            {
                AddUnitsView cView = new AddUnitsView();
                UnitAdderViewModel cVm = new UnitAdderViewModel(EventAggregator);
                cView.DataContext = cVm;
                this.CurrentViewLeft = cView;
            }
            else
            {
                this.CurrentViewLeft = memoryView;
            }// init new students view and view model

        }

        /// <summary>
        /// Determines, whether the student view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddFileDialogViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Student View" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddFileDialogViewCommandExecute(object parameter)
        {
            if (this.CurrentViewLeft == null)
            {
                OpenFileDialogView cView = new OpenFileDialogView();
                OpenFileDialogVM cVm = new OpenFileDialogVM(EventAggregator);
                cView.DataContext = cVm;
                this.CurrentViewRight = cView;

            }
            else
            {
                this.CurrentViewLeft = memoryView;
            }// init new students view and view model

        }
        #endregion
    }
}
