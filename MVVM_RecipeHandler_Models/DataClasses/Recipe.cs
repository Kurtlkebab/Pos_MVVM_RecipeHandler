using MVVM_RecipeHandler_Common.NotifyPropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVVM_RecipeHandler_Models.DataClasses
{
    /// <summary>
    /// Derives from the <see cref="NotifyPropertyChanged"/> class.
    /// </summary>
    public class Recipe : NotifyPropertyChanged
    {
        #region ------------- Fields, Constants, Delegates ------------------------

        /// <summary>
        /// name of the recipe.
        /// </summary>
        private string recipeName;

        /// <summary>
        /// short description of the recipe.
        /// </summary>
        private string recipeDescription;

        /// <summary>
        /// Id of the recipe.
        /// </summary>
        private int recipeid;

        /// <summary>
        /// string built from JPG Image.
        /// </summary>
        private string pictureURL;

        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        public Recipe()
        {
            this.IngredientsEx = new ObservableCollection<Ingredient>();
            this.Ingredients = new List<Ingredient>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        /// <param name="recipeName">name of the recipe.</param>
        /// <param name="recipeDescription">short description of the recipe..</param>    
        /// <param name="ingredients"> observable collection of ingredients</param>
        public Recipe(string recipeName, string recipeDescription, List<Ingredient> ingredients)
        {
            this.IngredientsEx = new ObservableCollection<Ingredient>();
            this.Ingredients = new List<Ingredient>();
            this.Ingredients = ingredients;
            this.recipeName = recipeName;
            this.recipeDescription = recipeDescription;       
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        /// <param name="recipeName">name of the recipe.</param>
        /// <param name="recipeDescription">short description of the recipe..</param>    
        /// <param name="pictureUrl"> Image as string from recipe</param>
        /// <param name="ingredients"> list of ingredients</param>
        public Recipe(string recipeName, string recipeDescription, string pictureUrl, List<Ingredient> ingredients)
        {
            this.LoadIngredientsEX(ingredients);
            this.Ingredients = new List<Ingredient>();
            this.Ingredients = ingredients;
            this.recipeName = recipeName;
            this.recipeDescription = recipeDescription;          
            this.pictureURL = pictureUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        /// <param name="recipeName">name of the recipe.</param>
        /// <param name="recipeDescription">short description of the recipe..</param>    
        /// <param name="pictureUrl"> Image as string from recipe</param>
        /// <param name="ingredients"> observable collection of ingredients</param>
        public Recipe(string recipeName, string recipeDescription, string pictureUrl, ObservableCollection<Ingredient> ingredients)
        {
            this.IngredientsEx = new ObservableCollection<Ingredient>();
            this.Ingredients = new List<Ingredient>();
            this.Ingredients = ingredients;
            this.recipeName = recipeName;
            this.recipeDescription = recipeDescription;
            this.pictureURL = pictureUrl;
        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------
        /// <summary>
        /// Gets or sets the list with all Ingredient data.
        /// </summary>
        public virtual ICollection<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets the Observable collection IngredientsEX 
        /// </summary>
        [NotMapped]
       public ObservableCollection<Ingredient> IngredientsEx { get; set; }

        /// <summary>
        /// Gets or sets the id of the recipe.
        /// </summary>
        public int RecipeId
        {
            get
            {
                return this.recipeid;
            }

            set
            {
                if (this.recipeid != value)
                {
                    this.recipeid = value;
                    this.OnPropertyChanged(nameof(this.RecipeId));
                }
            }
        }

        /// <summary>
        /// Gets or sets the recipe name of the recipe.
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
                    this.OnPropertyChanged(nameof(this.recipeName));               
                }
            }
        }

        /// <summary>
        /// Gets or sets the last name of the student.
        /// </summary>
        public string PictureURL
        {
            get
            {
                return this.pictureURL;
            }

            set
            {
                if (this.pictureURL != value)
                {
                    this.pictureURL = value;
                    this.OnPropertyChanged(nameof(this.pictureURL));
                }
            }
        }

        /// <summary>
        /// Gets or sets the last name of the student.
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
                    this.OnPropertyChanged(nameof(this.recipeDescription));           
                }
            }
        }

        #endregion

        #region ------------- Private helper --------------------------------------
        /// <summary>
        /// Loads ingredients from list to observable collection
        /// </summary>
        /// <param name="ingredients"> list to insert into observable collection</param>
        public virtual void LoadIngredientsEX(List<Ingredient> ingredients)
        {
            this.IngredientsEx = new ObservableCollection<Ingredient>();
            foreach (var item in ingredients)
            {
                this.IngredientsEx.Add(item);
            }         
        }
        #endregion--------------------------------------------------------------------
    }
}
