using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.Events;
using MVVM_RecipeHandler_Models.DataClasses;

namespace MVVM_RecipeHandler.ViewModels
{
    /// <summary>
    /// Displays the inspect current recipe view.
    /// Derives from <see cref="ViewModelBase"/> class.
    /// </summary>
    public class InspectCurrentRecipeViewModel : ViewModelBase
    {
        #region ------------- Fields, Constants, Delegates ------------------------
        
        /// <summary>
        /// selected recipe set from main button view event .
        /// </summary>       
        private Recipe selectedRecipe;
        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="InspectCurrentRecipeViewModel"/> class.
        /// </summary>
        /// /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public InspectCurrentRecipeViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // subscribe to event
            EventAggregator.GetEvent<SelectedRecipeChangedEvent>().Subscribe(this.OnRecipeDataChanged);
        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------
   
        /// <summary>
        /// Gets or sets the selected recipe.
        /// </summary>
        public Recipe SelectedRecipe
        {
            get
            {
                return this.selectedRecipe;
            }

            set
            {
                if (this.selectedRecipe != value)
                {
                    this.selectedRecipe = value;
                    this.OnPropertyChanged(nameof(this.SelectedRecipe));
               }
            }
        }

        #endregion

        #region ------------- Events ----------------------------------------------
  
        /// <summary>
        /// Event handler to notice changes in the current recipe data.
        /// </summary>
        /// <param name="recipe">Reference to the recipe data.</param>
        public void OnRecipeDataChanged(Recipe recipe)
        {          
            this.SelectedRecipe = recipe;
        }
        #endregion
    }
}
