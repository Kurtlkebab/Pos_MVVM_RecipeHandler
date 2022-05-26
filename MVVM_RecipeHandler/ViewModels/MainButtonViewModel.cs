using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.Events;
using MVVM_RecipeHandler_Common.Command;
using MVVM_RecipeHandler_Models.DataClasses;
using System;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

using System.Windows.Input;
using MVVM_RecipeHandler_EF6._0;
using System.Linq;
using Microsoft.Practices.Prism;
using System.Collections.Generic;

namespace MVVM_RecipeHandler.ViewModels
{
    /// <summary>
    /// Displays the students data in a list.
    /// Derives from <see cref="ViewModelBase"/> class.
    /// </summary>
    public class MainButtonViewModel : ViewModelBase
    {
        #region ------------- Fields, Constants, Delegates ------------------------
        /// <summary>
        /// Selected Recipe from Button
        /// </summary>
        private Recipe selectedRecipe;

        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="MainButtonViewModel"/> class.
        /// </summary>
        /// /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public MainButtonViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            EventAggregator.GetEvent<newRecipeEvent>().Subscribe(this.OnNewRecipe);

            // load ingredient data from db
            this.LoadRecipes();
           
            this.SelectedButtonCommand = new ActionCommand((value) => { this.SelectedButtonCommandExecute(value); }, this.SelectedButtonCommandCanExecute);
        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------

        /// <summary>
        /// Gets or sets the selected Recipe name from specific Recipe Button.
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

        /// <summary>
        /// Gets or sets the list with all recipe data.
        /// </summary>
        public ObservableCollection<Recipe> MyRecipeItems { get; set; }

        /// <summary>
        /// Gets the recipe from selected button command
        /// </summary>
        public ICommand SelectedButtonCommand { get; }

        #endregion

        #region ------------- Events ----------------------------------------------
   
        /// <summary>
        /// Event handler to notice changes in the current Recipe data.
        /// </summary>
        /// <param name="student">Reference to the student data.</param>
        public void OnNewRecipe(Recipe recipe)
        {
            MyRecipeItems.Add(recipe);
            this.OnPropertyChanged(nameof(MyRecipeItems));
        }
        #endregion

        #region ------------- Private helper --------------------------------------
        /// <summary>
        /// Generate Amount data from database
        /// </summary>
        private void LoadRecipes()
        {
            // init collection and add data from db
            this.MyRecipeItems = new ObservableCollection<Recipe>();
            using (var context = new RecipeContext())
            {
                var recipes = context.RecipesSet.SqlQuery("SELECT * FROM dbo.Recipes").ToList();
                foreach (var item in recipes)
                {
                    item.LoadIngredientsEX(item.Ingredients.ToList<Ingredient>());
                }
                MyRecipeItems.AddRange(recipes);
            }
        }


        #endregion

        #region ------------- Commands --------------------------------------------

        /// <summary>
        /// Determines, whether the specific Recipe Button command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool SelectedButtonCommandCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks a specific "Recipe" button.
        /// </summary>
        /// <param name="parameter">Data used by the command is Recipename of chossen recipe.</param>
        private void SelectedButtonCommandExecute(object parameter)
        {
            foreach (var item in MyRecipeItems)
            {
                if ((string)parameter == item.RecipeName)
                {
                    SelectedRecipe = item;
                }
            }

            EventAggregator.GetEvent<SelectedRecipeChangedEvent>().Publish(SelectedRecipe);
        }
        #endregion
    }
}
