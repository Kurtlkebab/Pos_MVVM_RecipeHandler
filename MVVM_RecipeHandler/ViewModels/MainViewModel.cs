using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.Views;
using MVVM_RecipeHandler_Common.Command;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM_RecipeHandler.ViewModels
{
    /// <summary>
    /// Main ViewModel of the application.
    /// Derives from the <see cref="ViewModelBase"/> class.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region ------------- Fields, Constants, Delegates ------------------------
    
        /// <summary>
        /// memoryView for switching Pages
        /// </summary>
        private UserControl memoryViewLeft;

        /// <summary>
        /// memoryView for switching Pages
        /// </summary>
        private UserControl memoryViewRight;
      
        /// <summary>
        /// memoryView for switching Pages
        /// </summary>
        private UserControl memoryViewBottom;
       
        /// <summary>
        /// memoryView for switching Pages
        /// </summary>
        private UserControl memoryViewLeftRightAdd;

        /// <summary>
        /// memoryView for switching Pages
        /// </summary>
        private UserControl memoryViewLeftAdd;

        /// <summary>
        /// memoryView for switching Pages
        /// </summary>
        private UserControl memoryViewRightAdd;

        /// <summary>
        /// memoryView for switching Pages
        /// </summary>
        private UserControl memoryViewBottomAdd;

        /// <summary>
        /// View that is currently bound to the left ContentControl.
        /// </summary>
        private UserControl currentViewLeft;

        /// <summary>
        /// View that is currently bound to the right ContentControl.
        /// </summary>
        private UserControl currentViewRight;

        /// <summary>
        /// View that is currently bound to the bottom ContentControl.
        /// </summary>
        private UserControl currentViewBottom;

        /// <summary>
        /// View that is currently bound to the left ContentControl for Recipe editor.
        /// </summary>
        private UserControl currentViewLeftAdd;

        /// <summary>
        /// View that is currently bound to the left ContentControl for Recipe editor.
        /// </summary>
        private UserControl currentViewLeftRightAdd;

        /// <summary>
        /// View that is currently bound to the rightContentControl for Recipe editor.
        /// </summary>
        private UserControl currentViewRightAdd;

        /// <summary>
        /// View that is currently bound to the bottom ContentControl for Recipe editor.
        /// </summary>
        private UserControl currentViewBottomAdd;

        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public MainViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            MainButtonView mainButtonView = new MainButtonView();
            MainButtonViewModel mainButtonVM = new MainButtonViewModel(EventAggregator);
            mainButtonView.DataContext = mainButtonVM;
            this.CurrentViewLeft = mainButtonView;

            InspectCurrentRecipeView inspectView = new InspectCurrentRecipeView();
            InspectCurrentRecipeViewModel inspectVM = new InspectCurrentRecipeViewModel(EventAggregator);
            inspectView.DataContext = inspectVM;
            this.CurrentViewBottom = inspectView;

            ShoppingCartView shoppingView = new ShoppingCartView();
            ShoppingCartViewModel shoppingVM = new ShoppingCartViewModel(EventAggregator);
            shoppingView.DataContext = shoppingVM;
            this.CurrentViewRight = shoppingView;

            this.AddAllViewCommand = new ActionCommand(this.AddAllViewCommandExecute, this.AddAllViewCommandCanExecute);
            this.AddMainViewCommand = new ActionCommand(this.AddMainViewCommandExecute, this.AddMainViewCommandCanExecute);
        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------
       
        /// <summary>
        /// Gets the student view loading button command.
        /// </summary>
        public ICommand AddMainViewCommand { get; private set; }
    
        /// <summary>
        /// Gets the student view loading button command.
        /// </summary>
        public ICommand AddAllViewCommand { get; private set; }

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
        public UserControl CurrentViewLeftAdd
        {
            get
            {
                return this.currentViewLeftAdd;
            }

            set
            {
                if (this.currentViewLeftAdd != value)
                {
                    this.currentViewLeftAdd = value;
                    this.OnPropertyChanged(nameof(this.currentViewLeftAdd));
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
        /// Gets or sets the view that is currently bound to the left ContentControl.
        /// </summary>
        public UserControl CurrentViewRightAdd
        {
            get
            {
                return this.currentViewRightAdd;
            }

            set
            {
                if (this.currentViewRightAdd != value)
                {
                    this.currentViewRightAdd = value;
                    this.OnPropertyChanged(nameof(this.CurrentViewRightAdd));
                }
            }
        }

        /// <summary>
        /// Gets or sets the view that is currently bound to the left ContentControl.
        /// </summary>
        public UserControl CurrentViewLeftRightAdd
        {
            get
            {
                return this.currentViewLeftRightAdd;
            }

            set
            {
                if (this.currentViewLeftRightAdd != value)
                {
                    this.currentViewLeftRightAdd = value;
                    this.OnPropertyChanged(nameof(this.CurrentViewLeftRightAdd));
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

        /// <summary>
        /// Gets or sets the view that is currently bound to the right ContentControl.
        /// </summary>
        public UserControl CurrentViewBottomAdd
        {
            get
            {
                return this.currentViewBottomAdd;
            }

            set
            {
                if (this.currentViewBottomAdd != value)
                {
                    this.currentViewBottomAdd = value;
                    this.OnPropertyChanged(nameof(this.CurrentViewBottomAdd));
                }
            }
        }
        #endregion

        #region ------------- Commands --------------------------------------------
   
        /// <summary>
        /// Determines, whether the add all view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddAllViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the add recipe button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddAllViewCommandExecute(object parameter)
        {
            this.memoryViewLeft = this.CurrentViewLeft;
            this.memoryViewRight = this.CurrentViewRight;
            this.memoryViewBottom = this.CurrentViewBottomAdd;

            this.CurrentViewBottom = null;
            this.CurrentViewLeft = null;
            this.CurrentViewRight = null;

            if (this.memoryViewLeftAdd == null)
            {
                AddUnitsView addUView = new AddUnitsView();
                UnitAdderViewModel addUVM = new UnitAdderViewModel(EventAggregator);
                addUView.DataContext = addUVM;
                this.CurrentViewLeftAdd = addUView;
                this.memoryViewLeftAdd = addUView;
            }
            else
            {
                this.CurrentViewLeftAdd = this.memoryViewLeftAdd;
            }

            if (this.memoryViewLeftRightAdd == null)
            {
                AddIngredientsView addIngredientsView = new AddIngredientsView();
                IngredientAdderViewModel vm = new IngredientAdderViewModel(EventAggregator);
                addIngredientsView.DataContext = vm;
                this.CurrentViewLeftRightAdd = addIngredientsView;
                this.memoryViewLeftRightAdd = addIngredientsView;
            }
            else
            {
                this.CurrentViewLeftRightAdd = this.memoryViewLeftRightAdd;
            }

            if (this.memoryViewRightAdd == null)
            {
                AddRecipeView addRView = new AddRecipeView();
                AddRecipeViewModel addRVM = new AddRecipeViewModel(EventAggregator);
                addRView.DataContext = addRVM;
                this.CurrentViewRightAdd = addRView;
                this.memoryViewRightAdd = addRView;
            }
            else
            {
                this.CurrentViewRightAdd = this.memoryViewRightAdd;
            }

            if (this.memoryViewBottomAdd == null)
            {
                OpenFileDialogView openV = new OpenFileDialogView();
                OpenFileDialogVM openVM = new OpenFileDialogVM(EventAggregator);
                openV.DataContext = openVM;
                this.CurrentViewBottomAdd = openV;
                this.memoryViewBottomAdd = openV;
            }
            else
            {
                this.CurrentViewBottomAdd = this.memoryViewBottomAdd;
            }     
        }

        /// <summary>
        /// Determines, whether the student view loading command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddMainViewCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Home" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddMainViewCommandExecute(object parameter)
        {
           this.memoryViewLeftAdd = this.CurrentViewLeftAdd;
           this.memoryViewLeftRightAdd = this.CurrentViewLeftRightAdd;
           this.memoryViewRightAdd = this.CurrentViewRightAdd;
            this.memoryViewBottomAdd = this.CurrentViewBottomAdd;

            this.CurrentViewLeftAdd = null;
            this.CurrentViewLeftRightAdd = null;
            this.CurrentViewRightAdd = null;
            this.CurrentViewBottomAdd = null;

            if (this.memoryViewLeft == null)
            {
                MainButtonView mainButtonView = new MainButtonView();
                MainButtonViewModel mainButtonVM = new MainButtonViewModel(EventAggregator);
                mainButtonView.DataContext = mainButtonVM;
                this.CurrentViewLeft = mainButtonView;
                this.memoryViewLeft = mainButtonView;
            }
            else
            {
                this.CurrentViewLeft = this.memoryViewLeft;
            }

            if (this.memoryViewBottom == null)
            {
                InspectCurrentRecipeView inspectView = new InspectCurrentRecipeView();
                InspectCurrentRecipeViewModel inspectVM = new InspectCurrentRecipeViewModel(EventAggregator);
                inspectView.DataContext = inspectVM;
                this.CurrentViewBottom = inspectView;
                this.memoryViewBottom = inspectView;
            }
            else
            {
                this.CurrentViewBottom = this.memoryViewBottom;
            }

            if (this.memoryViewRight == null)
            {
                ShoppingCartView shoppingView = new ShoppingCartView();
                ShoppingCartViewModel shoppingVM = new ShoppingCartViewModel(EventAggregator);
                shoppingView.DataContext = shoppingVM;
                this.CurrentViewRight = shoppingView;
                this.memoryViewRight = shoppingView;
            }
            else
            {
                this.CurrentViewRight = this.memoryViewRight;
            }          
        }        
        #endregion
    }
}
