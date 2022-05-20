using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.Events;
using MVVM_RecipeHandler_Common.Command;
using MVVM_RecipeHandler_Models.DataClasses;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
namespace MVVM_RecipeHandler.ViewModels
{
    /// <summary>
    /// Displays the students data in a list.
    /// Derives from <see cref="ViewModelBase"/> class.
    /// </summary>
    public class AddRecipeViewModel : ViewModelBase
    {
        #region ------------- Fields, Constants, Delegates ------------------------
        /// <summary>
        /// new Ingredient from textbox.
        /// </summary>
        public Recipe newRecipe;
        public string selectedIngredient;
        public string selectedUnit;
        private string recipeName;
        private string recipeDescription;
        private string recipeImageURL;
        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRecipeViewModel"/> class.
        /// </summary>
        /// /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public AddRecipeViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
           
            this.Ingredients = new ObservableCollection<Ingredient>();
            this.Units = new ObservableCollection<Unit>();


            Ingredient ing1 = new Ingredient("kas");
            Ingredient ing2 = new Ingredient("kas1");
            Unit un1 = new Unit("kg");
            Unit un2 = new Unit("kg1");
            Ingredients.Add(ing1);

            Ingredients.Add(ing2);
           
            Units.Add(un1);
            Units.Add(un2);
            newRecipe = new Recipe("adsad", "adsad2123" , - 1, "adasdadadasdas");
            newRecipe.Ingredients.Add(ing1);
            newRecipe.Ingredients.Add(ing2);
            // load ingredient data from db
            this.LoadRecipes();

            // hookup command to assoiated methode

            this.AddRecipeIngredientCommand = new ActionCommand(this.AddToRecipeButtonCommandExecute, this.AddToRecipeButtonCommandCanExecute);

            this.AddRecipeDescriptionNameCommand = new ActionCommand(this.AddToRecipeDescriptionNameCommandExecute, this.AddToRecipeDescriptionNameCommandCanExecute);
           
            this.AddRecipeCommand = new ActionCommand(this.AddRecipeButtonCommandExecute, this.AddRecipeButtonCommandCanExecute);

            // subscribe to Unitdatachangeevent
            EventAggregator.GetEvent<UnitDataChangedEvent>().Subscribe(this.OnUnitDataChanged);

            // subscribe to IngredientDataChangeEvent
            EventAggregator.GetEvent<IngredientDataChangedEvent>().Subscribe(this.OnIngredientDataChanged);

            EventAggregator.GetEvent<ImageStringDataChangedEvent>().Subscribe(this.OnImageStringDataChanged);


        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------
        /// <summary>
        /// Gets or sets the list with all Recipe data.
        /// </summary>
        public ObservableCollection<Recipe> MyRecipeItems { get; set; }

        /// <summary>
        /// Gets or sets the list with all Ingredients data.
        /// </summary>
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets the list with all Unit data.
        /// </summary>
        public ObservableCollection<Unit> Units { get; set; }

        /// <summary>
        /// Gets the Add recipe ingredient command.
        /// </summary>
        public ICommand AddRecipeIngredientCommand { get; }
        /// <summary>
        /// Gets the Add recipe ingredient command.
        /// </summary>
        public ICommand AddRecipeCommand { get; }
        /// <summary>
        /// Gets the Add recipe description+name command.
        /// </summary>
        public ICommand AddRecipeDescriptionNameCommand { get; }

        /// <summary>
        /// Gets or sets the current edited new Recipe
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

                    //EventAggregator.GetEvent<IngredientDataChangedEvent>().Publish(SelectedIngredient);
                }
            }
        }
        public string SelectedUnit
        {
            get
            {
                return this.selectedUnit;
            }

            set
            {
                if (this.selectedUnit != value)
                {
                    this.selectedUnit = value;
                    this.OnPropertyChanged(nameof(this.SelectedUnit));                 
                   
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

        public string RecipeImageURL {
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
        /// Gets or sets the selected Ingredient full name from combo box.
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
        /// Event handler to notice changes in the current Unit data.
        /// </summary>
        /// <param name="unit">Reference to the unit data.</param>
        public void OnUnitDataChanged(Unit unit)
        {
            this.Units.Add(unit);
        }

        /// <summary>
        /// Event handler to notice changes in the current Unit data.
        /// </summary>
        /// <param name="unit">Reference to the unit data.</param>
        public void OnImageStringDataChanged(string ImageString)
        {
            this.RecipeImageURL=ImageString;
            NewRecipe.PictureURL = this.RecipeImageURL;
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

            Recipe rez1 = new Recipe("eierspeis", "eier in pfanne hauen");
            MyRecipeItems.Add(rez1);

        }

        public string ToTxt()
        {
            string forTxtFile;
            forTxtFile = "Rezeptname: " + NewRecipe.RecipeName + "\n" + "Rezeptbeschreibung: " + NewRecipe.RecipeDescription + "\n" + "PictureUrl: " + NewRecipe.PictureURL+"\n\n";
            string IngredientsForTxt="Zutaten: \n";
            foreach (Ingredient ing in NewRecipe.Ingredients)
            {
                IngredientsForTxt += "Zutatenname: "+ing.IngredientName+"\n";
                if (ing.IngredientUnit != null)
                {
                    IngredientsForTxt += "Zutateneinheit: " + ing.IngredientName + "\n";
                }
                if (ing.Amount != null)
                {
                    IngredientsForTxt += "Menge: " + ing.Amount + "\n";
                }
            }
            forTxtFile=forTxtFile + IngredientsForTxt;
            return forTxtFile;
        }
        #endregion

        #region ------------- Commands --------------------------------------------
        /// <summary>
        /// Determines, whether the add to recipe command can be executed.
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
            NewRecipe.RecipeDescription = this.RecipeDescription;
            this.OnPropertyChanged(nameof(this.RecipeDescription));
            NewRecipe.RecipeName = this.RecipeName;
            NewRecipe.PictureURL = this.RecipeImageURL;
            this.OnPropertyChanged(nameof(this.newRecipe.RecipeDescription));
          
        }



        /// <summary>
        /// Determines, whether the add Recipe command can be executed.
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
        /// Occurs, when the user clicks the "Add recipe" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddToRecipeButtonCommandExecute(object parameter)
        {
          
            NewRecipe.Ingredients.Add(new Ingredient(SelectedIngredient, Amount, SelectedUnit));
        }

        /// <summary>
        /// Determines, whether the add Recipe command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddRecipeButtonCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Add recipe" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddRecipeButtonCommandExecute(object parameter)
        {
            EventAggregator.GetEvent<newRecipeEvent>().Publish(NewRecipe);
            ToTxt();
            
        }
        #endregion
    }
}
