using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.Events;
using MVVM_RecipeHandler_Common.Command;
using MVVM_RecipeHandler_Models.DataClasses;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MVVM_RecipeHandler_EF6._0;

namespace MVVM_RecipeHandler.ViewModels
{
    /// <summary>
    /// Displays the students data in a list.
    /// Derives from <see cref="ViewModelBase"/> class.
    /// </summary>
    public class InspectCurrentRecipeViewModel : ViewModelBase
    {
        #region ------------- Fields, Constants, Delegates ------------------------
        /// <summary>
        /// new Ingredient from textbox.
        /// </summary>
       
        public Recipe newRecipe;
        public Recipe selectedRecipe;
        public string selectedIngredient;
        public string selectedUnit;
        private string recipeName;
        private string recipeDescription;
        private string recipeImageURL;
        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="MainButtonViewModel"/> class.
        /// </summary>
        /// /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public InspectCurrentRecipeViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            

            // hookup command to assoiated 
            EventAggregator.GetEvent<SelectedRecipeChangedEvent>().Subscribe(this.OnUnitDataChanged);


            // subscribe to event
        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------
        /// <summary>
        /// Gets or sets the list with all student data.
        /// </summary>
        public ObservableCollection<Recipe> MyRecipeItems { get; set; }

        /// <summary>
        /// Gets or sets the list with all student data.
        /// </summary>
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets the list with all Unit data.
        /// </summary>
        public ObservableCollection<Unit> Units { get; set; }

        public ICommand AddRecipeIngredientCommand { get; }
        public ICommand AddRecipeDescriptionNameCommand { get; }

        /// <summary>
        /// Gets the save students button command.
        /// </summary>
        public ICommand AddUnitCommand { get; private set; }

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

                    //EventAggregator.GetEvent<IngredientDataChangedEvent>().Publish(SelectedIngredient);
                }
            }
        }
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

                    //EventAggregator.GetEvent<IngredientDataChangedEvent>().Publish(SelectedIngredient);
                }
            }
        }

        public string RecipeName
        {
            get
            {
                return this.recipeName;
            }

            set
            {
                if (this.recipeName != value)
                {
                    this.recipeName = value;
                    this.OnPropertyChanged(nameof(this.RecipeName));

                    //EventAggregator.GetEvent<IngredientDataChangedEvent>().Publish(SelectedIngredient);
                }
            }
        }

        public string RecipeDescription
        {
            get
            {
                return this.recipeDescription;
            }

            set
            {
                if (this.recipeDescription != value)
                {
                    this.recipeDescription = value;
                    this.OnPropertyChanged(nameof(this.RecipeDescription));

                    //EventAggregator.GetEvent<IngredientDataChangedEvent>().Publish(SelectedIngredient);
                }
            }
        }

        public string RecipeImageURL
        {
            get
            {
                return this.recipeImageURL;
            }

            set
            {
                if (this.recipeImageURL != value)
                {
                    this.recipeImageURL = value;
                    this.OnPropertyChanged(nameof(this.RecipeImageURL));

                    //EventAggregator.GetEvent<IngredientDataChangedEvent>().Publish(SelectedIngredient);
                }
            }
        }

        public string Amount { get; set; }

        /// <summary>
        /// Gets or sets the selected student full name from combo box.
        /// </summary>
        public string SelectedIngredient
        {
            get
            {

                return this.selectedIngredient;
            }

            set
            {
                if (this.selectedIngredient != value)
                {
                    this.selectedIngredient = value;
                    this.OnPropertyChanged(nameof(this.SelectedIngredient));
                    //this.selectedIngredient.Amount = Amount;
                    //this.selectedIngredient.IngredientUnit = SelectedUnit;
                    //EventAggregator.GetEvent<IngredientDataChangedEvent>().Publish(SelectedIngredient);
                }
            }
        }

        #endregion

        #region ------------- Events ----------------------------------------------
        /// <summary>
        /// Event handler to notice changes in the current ingredient data.
        /// </summary>
        /// <param name="ingredient">Reference to the ingredient data.</param>
        public void OnIngredientDataChanged(Ingredient ingredient)
        {
            this.Ingredients.Add(ingredient);
        }


        /// <summary>
        /// Event handler to notice changes in the current ingredient data.
        /// </summary>
        /// <param name="ingredient">Reference to the ingredient data.</param>
        public void OnUnitDataChanged(Recipe recipe)
        {
           
                //foreach (var item in recipe.IngredientsEx)
                //{
                   
                //    Ingredients.Add(item);
                //}
            
               
            this.SelectedRecipe = recipe;
            this.recipeImageURL = recipe.PictureURL;
            this.recipeName = recipe.RecipeName;
            this.recipeDescription = recipe.RecipeDescription;
        }
        #endregion

        #region ------------- Private helper --------------------------------------
        /// <summary>
        /// Generate Amount data from db.
        /// </summary>
        private void LoadRecipes()
        {
            // init collection and add data from db
            this.MyRecipeItems = new ObservableCollection<Recipe>();

            //Recipe rez1 = new Recipe("eierspeis", "eier in pfanne hauen");
            //MyRecipeItems.Add(rez1);

        }
        #endregion

        #region ------------- Commands --------------------------------------------
        /// <summary>
        /// Determines, whether the add amount command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddToRecipeDescriptionNameCommandCanExecute(object parameter)
        {
            //if (this.Ingredients.Any(s => s.StudentChanged))
            //{
            //    return true;
            //}

            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Add to recipe" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddToRecipeDescriptionNameCommandExecute(object parameter)
        {
            newRecipe.RecipeDescription = this.RecipeDescription;
            this.OnPropertyChanged(nameof(this.RecipeDescription));
            newRecipe.RecipeName = this.RecipeName;
            newRecipe.PictureURL = this.RecipeImageURL;
            this.OnPropertyChanged(nameof(this.newRecipe.RecipeDescription));

        }



        /// <summary>
        /// Determines, whether the add amount command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddToRecipeButtonCommandCanExecute(object parameter)
        {
            //if (this.Ingredients.Any(s => s.StudentChanged))
            //{
            //    return true;
            //}

            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Add to recipe" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddToRecipeButtonCommandExecute(object parameter)
        {

            newRecipe.Ingredients.Add(new Ingredient(selectedIngredient, Amount, selectedUnit));
            //Ingredients.Add(new Ingredient(selectedIngredient, Amount, selectedUnit));
        }
        #endregion
    }
}
