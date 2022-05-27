using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.Events;
using MVVM_RecipeHandler_Common.Command;
using MVVM_RecipeHandler_EF6._0;
using MVVM_RecipeHandler_Models.DataClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_RecipeHandler.ViewModels
{
    /// <summary>
    /// Displays the students data in a list.
    /// Derives from <see cref="ViewModelBase"/> class.
    /// </summary>
    public class IngredientAdderViewModel : ViewModelBase
    {
        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="IngredientAdderViewModel"/> class.
        /// </summary>
        /// /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public IngredientAdderViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // load ingredient data from db
            this.LoadIngredients();

            // hookup command to assoiated methode
            this.AddIngredientCommand = new ActionCommand(this.AddIngredientCommandExecute, this.AddIngredientCommandCanExecute);

            // subscribe to event
            EventAggregator.GetEvent<IngredientDataChangedEvent>().Subscribe(this.OnIngredientDataChange, ThreadOption.UIThread);
        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------
        /// <summary>
        /// Gets or sets the list with all Ingredient data.
        /// </summary>
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Gets the Add Ingredient button command.
        /// </summary>
        public ICommand AddIngredientCommand { get; private set; }

        /// <summary>
        /// Gets or sets the NewIngredient from Textbox
        /// </summary>
        public string NewIngredient { get; set; }
        #endregion

        #region ------------- Events ----------------------------------------------
        /// <summary>
        /// Event handler to notice changes in the current ingredient data.
        /// </summary>
        /// <param name="ingredient">Reference to the student data.</param>
        public void OnIngredientDataChange(Ingredient ingredient)
        {
            if (ingredient.Id == -1)
            {
                ingredient.Id = this.Ingredients.Max(s => s.Id) + 1;
                this.Ingredients.Add(ingredient);
            }
            else
            {
                var ingredientToUpdate = this.Ingredients.FirstOrDefault(s => s.Id == ingredient.Id);
                ingredientToUpdate.IngredientName = ingredient.IngredientName;
                ingredientToUpdate.Id = ingredient.Id;
            }
        }
        #endregion

        #region ------------- Private helper --------------------------------------
        /// <summary>
        /// Generate Ingredient data.
        /// </summary>
        private void LoadIngredients()
        {
            this.Ingredients = new ObservableCollection<Ingredient>();
            using (var context = new RecipeContext())
            {
                var ingredients = context.IngredientsSet.SqlQuery("SELECT * FROM dbo.Ingredients").ToList();
                foreach (var item in ingredients)
                {
                    this.Ingredients.Add(item);
                }             
            }   
        }
        #endregion

        #region ------------- Commands --------------------------------------------
        /// <summary>
        /// Determines, whether the add ingredients command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddIngredientCommandCanExecute(object parameter)
        {
            Ingredient ingredient;
            ingredient = new Ingredient(this.NewIngredient);
            Ingredient checkIfIngExists = this.Ingredients.FirstOrDefault(s => s.IngredientName == ingredient.IngredientName);

            if (checkIfIngExists == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Add Ingredient" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddIngredientCommandExecute(object parameter)
        {
            Ingredient ingredient;                     
            ingredient = new Ingredient(this.NewIngredient);
            this.Ingredients.Add(ingredient);

            // publish event when new ingredient is added
            EventAggregator.GetEvent<IngredientDataChangedEvent>().Publish(ingredient);
            try
            {
                using (var context = new RecipeContext())
                {
                    context.IngredientsSet.Add(ingredient);
                    context.SaveChanges();
                }
            }catch (Exception ex)
            {
            }
        }
        }
        #endregion
    }
