using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.Events;
using MVVM_RecipeHandler_Common.Command;
using MVVM_RecipeHandler_Models.DataClasses;
using System;
using System.IO;
using System.Windows.Input;

namespace MVVM_RecipeHandler.ViewModels
{
    /// <summary>
    /// Displays the students data in a list.
    /// Derives from <see cref="ViewModelBase"/> class.
    /// </summary>
    public class ShoppingCartViewModel : ViewModelBase
    {
        #region ------------- Fields, Constants, Delegates ------------------------
        
        /// <summary>
        /// new Ingredient from textbox.
        /// </summary>
        private Recipe newRecipe;
        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartViewModel"/> class.
        /// </summary>
        /// /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public ShoppingCartViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // hookup command to assoiated methode
            this.AddToCartCommand = new ActionCommand(this.AddToCartCommandExecute, this.AddToCartCommandCanExecute);

            // subscribe to event
            EventAggregator.GetEvent<SelectedRecipeChangedEvent>().Subscribe(this.OnRecipeDataChanged);
        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------

        /// <summary>
        /// Gets the Add to cart button command.
        /// </summary>
        public ICommand AddToCartCommand { get; }
       
        /// <summary>
        /// Gets or sets the new recipe from selected recipe changed event
        /// </summary>
        public Recipe NewRecipe
        {
            get
            {
                return this.newRecipe;
            }

            set
            {
                if (this.newRecipe != value)
                {
                    this.newRecipe = value;
                    this.OnPropertyChanged(nameof(this.NewRecipe));
               }
            }
        }
        #endregion

        #region ------------- Events ----------------------------------------------
        /// <summary>
        /// Event handler to notice changes in the current recipe data.
        /// </summary>
        /// <param name="recipe">Reference to the ingredient data.</param>
        public void OnRecipeDataChanged(Recipe recipe)
        {
            this.NewRecipe = recipe;
        }
        #endregion

        #region ------------- Private helper --------------------------------------
        /// <summary>
        /// Builds an string from <see cref="Recipe"/> object.
        /// </summary>
        /// <returns> string to write to text file</returns>
        private string ToTxt()
        {
            string forTxtFile;
            forTxtFile = "\n Rezeptname: " + this.NewRecipe.RecipeName + "\n" + "Rezeptbeschreibung: " + this.NewRecipe.RecipeDescription + "\n" + "\n\n";
            string ingredientsForTxt = "Zutaten: " + Environment.NewLine;
            foreach (Ingredient ing in this.NewRecipe.Ingredients)
            {
                ingredientsForTxt += "Zutatenname: " + ing.IngredientName + "\n";
                if (ing.IngredientUnit != null)
                {
                    ingredientsForTxt += "Zutateneinheit: " + ing.IngredientUnit + "\n";
                }

                if (ing.Amount != null)
                {
                    ingredientsForTxt += "Menge: " + ing.Amount + "\n";
                }
            }

            forTxtFile = forTxtFile + ingredientsForTxt;
            return forTxtFile;
        }

        #endregion

        #region ------------- Commands --------------------------------------------

        /// <summary>
        /// Determines, whether the add amount command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddToCartCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Add to recipe" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddToCartCommandExecute(object parameter)
        {
            string forTextFile = this.ToTxt();
            DateTime dateOnly = DateTime.Today.Date;
            string path = @".\" + dateOnly.ToString("d") + "_Einkaufsliste.txt";
            using (StreamWriter sw = new StreamWriter(path, true))
            {               
                sw.Write(forTextFile);
            }              
        }
        #endregion
    }
}
