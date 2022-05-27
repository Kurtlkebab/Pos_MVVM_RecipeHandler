using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.Events;
using MVVM_RecipeHandler.Views;
using MVVM_RecipeHandler_Common.Command;
using MVVM_RecipeHandler_EF6._0;
using MVVM_RecipeHandler_Models.DataClasses;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
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
        private Recipe newRecipe;

        /// <summary>
        /// selected ingredient
        /// </summary>
        private string selectedIngredient;

        /// <summary>
        /// selected unit
        /// </summary>
        private string selectedUnit;

        /// <summary>
        /// recipe name
        /// </summary>
        private string recipeName;

        /// <summary>
        /// recipe description
        /// </summary>
        private string recipeDescription;

        /// <summary>
        /// recipe image url
        /// </summary>
        private string recipeImageURL;

        /// <summary>
        /// recipe image url
        /// </summary>
        private string selectedSave;

        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRecipeViewModel"/> class.
        /// </summary>
        /// /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public AddRecipeViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.FileSaveModes = new ObservableCollection<string>();
            this.FileSaveModes.Add("In Datenbank speichern");
            this.FileSaveModes.Add("In Text Datei speichern");
            this.Ingredients = new ObservableCollection<Ingredient>();
            this.Units = new ObservableCollection<Unit>();
            ObservableCollection<Ingredient> newO = new ObservableCollection<Ingredient>();

            this.newRecipe = new Recipe(string.Empty, string.Empty, string.Empty, newO);
          
            // load ingredient data from db
            this.LoadRecipesIngredients();

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
        /// Gets or sets the list of file save modes
        /// </summary>
        public ObservableCollection<string> FileSaveModes { get; set; }

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
               }
            }
        }

        /// <summary>
        /// Gets or sets selected unit
        /// </summary>
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
              }
            }
        }

        /// <summary>
        /// Gets or sets recipe name
        /// </summary>
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
                }
            }
        }

        /// <summary>
        /// Gets or sets recipe name
        /// </summary>
        public string SelectedSave
        {
            get
            {
                return this.selectedSave;
            }

            set
            {
                if (this.selectedSave != value)
                {
                    this.selectedSave = value;
                    this.OnPropertyChanged(nameof(this.SelectedSave));
                }
            }
        }

        /// <summary>
        /// Gets or sets recipe description
        /// </summary>
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
               }
            }
        }

        /// <summary>
        /// Gets or sets recipe Image url
        /// </summary>
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
                }
            }
        }

        /// <summary>
        /// Gets or sets string amount
        /// </summary>
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
            Ingredient checkIfIngExists = this.Ingredients.FirstOrDefault(s => s.IngredientName == ingredient.IngredientName);
            if (checkIfIngExists == null)
            {
                this.Ingredients.Add(ingredient);
            }         
        }

        /// <summary>
        /// Event handler to notice changes in the current Unit data.
        /// </summary>
        /// <param name="unit">Reference to the unit data.</param>
        public void OnUnitDataChanged(Unit unit)
        {
            Unit checkIfUnitExists = this.Units.FirstOrDefault(s => s.UnitName == unit.UnitName);
            if (checkIfUnitExists == null)
            {
                this.Units.Add(unit);
            }             
        }

        /// <summary>
        /// Event handler to notice changes in the current image string data.
        /// </summary>
        /// <param name="imageString">Reference to the image string data.</param>
        public void OnImageStringDataChanged(string imageString)
        {
            this.RecipeImageURL = imageString;
            this.NewRecipe.PictureURL = this.RecipeImageURL;
        }
        #endregion

        #region ------------- Private helper --------------------------------------
        /// <summary>
        /// Generate Amount data from database.
        /// </summary>
        private void LoadRecipesIngredients()
        {
            this.MyRecipeItems = new ObservableCollection<Recipe>();
            using (var context = new RecipeContext())
            {
                var ingredients = context.IngredientsSet.SqlQuery("SELECT * FROM dbo.Ingredients").ToList();
                
                foreach (var item in ingredients)
                {
                    this.Ingredients.Add(item);
                }

                var units = context.UnitsSet.SqlQuery("SELECT * FROM dbo.Units").ToList();

                foreach (var item in units)
                {
                    this.Units.Add(item);
                }

                var recipes = context.RecipesSet.SqlQuery("SELECT * FROM dbo.Recipes").ToList();

                foreach (var item in recipes)
                {
                    this.MyRecipeItems.Add(item);
                }
            }
        }

        /// <summary>
        /// Builds an string from <see cref="Recipe"/> object.
        /// </summary>
        /// <returns> string to write to text file</returns>
        private string ToTxt()
        {
            string forTxtFile;
            forTxtFile = "Rezeptname: " + this.NewRecipe.RecipeName + "\n" + "Rezeptbeschreibung: " + this.NewRecipe.RecipeDescription + "\n" + "PictureUrl: " + this.NewRecipe.PictureURL + "\n\n";
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
        /// Determines, whether the add to recipe command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddToRecipeDescriptionNameCommandCanExecute(object parameter)
        {         
            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Add to recipe" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddToRecipeDescriptionNameCommandExecute(object parameter)
        {
            this.NewRecipe.RecipeDescription = this.RecipeDescription;
            this.OnPropertyChanged(nameof(this.RecipeDescription));
            this.NewRecipe.RecipeName = this.RecipeName;
            this.NewRecipe.PictureURL = this.RecipeImageURL;
            this.OnPropertyChanged(nameof(this.newRecipe.RecipeDescription));         
        }

        /// <summary>
        /// Determines, whether the add Recipe command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddToRecipeButtonCommandCanExecute(object parameter)
        {        
            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Add to recipe" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddToRecipeButtonCommandExecute(object parameter)
        {        
            this.NewRecipe.IngredientsEx.Add(new Ingredient(this.SelectedIngredient, this.Amount, this.SelectedUnit));
            this.OnPropertyChanged(nameof(this.NewRecipe.Ingredients));
        }

        /// <summary>
        /// Determines, whether the add Recipe command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddRecipeButtonCommandCanExecute(object parameter)
        {           
            Recipe checkIfRecExists = this.MyRecipeItems.FirstOrDefault(s => s.RecipeName == this.NewRecipe.RecipeName);

            if (checkIfRecExists == null)
            {
                if (this.NewRecipe.PictureURL != null)
                {
                    if (this.NewRecipe.PictureURL.Length > 20 && this.NewRecipe.RecipeDescription != null && this.NewRecipe.RecipeName != null)
                    {
                        return true;
                    }
                }              
            }  
            
            return false;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Add recipe" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddRecipeButtonCommandExecute(object parameter)
        {
            EventAggregator.GetEvent<newRecipeEvent>().Publish(this.NewRecipe);
            string forTxtFile = this.ToTxt();
            string path = @".\" + this.NewRecipe.RecipeName + "_Einkaufsliste.txt";

            if (this.selectedSave == "In Text Datei speichern")
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.Write(forTxtFile);
                }
            }
            else
            {
                using (var context = new RecipeContext())
                {
                    context.RecipesSet.Add(this.NewRecipe);
                    context.SaveChanges();
                }
            }   
        }
        #endregion
    }
}
