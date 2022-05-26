using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.Events;
using MVVM_RecipeHandler.Views;
using MVVM_RecipeHandler_Common.Command;
using MVVM_RecipeHandler_Models.DataClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class MainViewModel : ViewModelBase
    {
        #region ------------- Fields, Constants, Delegates ------------------------
        private UserControl memoryView;
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
           
            MainButtonView mView = new MainButtonView();
            MainButtonViewModel mVm = new MainButtonViewModel(EventAggregator);
            mView.DataContext = mVm;


           

            this.CurrentViewLeft = mView;

            InspectCurrentRecipeView iView = new InspectCurrentRecipeView();
            InspectCurrentRecipeViewModel iVm = new InspectCurrentRecipeViewModel(EventAggregator);
            iView.DataContext = iVm;

            this.CurrentViewBottom = iView;

            ShoppingCartView cView = new ShoppingCartView();
            ShoppingCartViewModel cVm = new ShoppingCartViewModel(EventAggregator);
            cView.DataContext = cVm;

            this.CurrentViewRight = cView;

            // hookup command to assoiated methode
          
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
        /// Occurs, when the user clicks the "Rezept erstellen" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddAllViewCommandExecute(object parameter)
        {
            this.CurrentViewBottom = null;
            this.CurrentViewLeft = null;
            this.CurrentViewRight = null;

            if (this.CurrentViewLeftAdd == null)
            {
                AddUnitsView cView = new AddUnitsView();
                UnitAdderViewModel cVm = new UnitAdderViewModel(EventAggregator);
                cView.DataContext = cVm;
                this.CurrentViewLeftAdd = cView;
            }
            else
            {
                this.CurrentViewLeftAdd = memoryView;
            }


           
            AddIngredientsView addIngredientsView = new AddIngredientsView();
            IngredientAdderViewModel vm = new IngredientAdderViewModel(EventAggregator);
            addIngredientsView.DataContext = vm;
            this.CurrentViewLeftRightAdd = addIngredientsView;
            AddRecipeView addRView = new AddRecipeView();
            AddRecipeViewModel Advm = new AddRecipeViewModel(EventAggregator);
            addRView.DataContext = Advm;
            this.CurrentViewRightAdd = addRView;
            OpenFileDialogView openV = new OpenFileDialogView();
            OpenFileDialogVM openVM = new OpenFileDialogVM(EventAggregator);
            openV.DataContext = openVM;
            this.CurrentViewBottomAdd = openV;
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
            this.CurrentViewLeftAdd = null;
            this.CurrentViewLeftRightAdd = null;
            this.CurrentViewRightAdd = null;
            this.CurrentViewBottomAdd = null;
            MainButtonView mView = new MainButtonView();
            MainButtonViewModel mVm = new MainButtonViewModel(EventAggregator);
            mView.DataContext = mVm;
            this.CurrentViewLeft = mView;
            InspectCurrentRecipeView iView = new InspectCurrentRecipeView();
            InspectCurrentRecipeViewModel iVm = new InspectCurrentRecipeViewModel(EventAggregator);
            iView.DataContext = iVm;
            this.CurrentViewBottom = iView;
            ShoppingCartView cView = new ShoppingCartView();
            ShoppingCartViewModel cVm = new ShoppingCartViewModel(EventAggregator);
            cView.DataContext = cVm;
            this.CurrentViewRight = cView;           
        }        
        #endregion
    }
}
