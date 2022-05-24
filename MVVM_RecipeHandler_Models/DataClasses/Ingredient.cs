using MVVM_RecipeHandler_Common.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_RecipeHandler_Models.DataClasses
{
 /// <summary>
 /// Derives from the <see cref="NotifyPropertyChanged"/> class.
 /// </summary>
    public class Ingredient : NotifyPropertyChanged
    {
        #region ------------- Fields, Constants, Delegates ------------------------
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       // public int? RecipeId { get; set; }
        //public Recipe Recipe { get; set; }


        /// <summary>
        /// Name of the Ingredient.
        /// </summary>
        private string ingredientName;

        /// <summary>
        /// unit of the Ingredient.
        /// </summary>
        private string ingredientUnit;

        /// <summary>
        /// Amount of the Ingredient.
        /// </summary>
        private string amount;

        /// <summary>
        /// Id of the Ingredient.
        /// </summary>
        private int id;

        
        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="Ingredient"/> class.
        /// </summary>
        /// <param name="ingredientName"> name of ingredient</param>
        /// <param name="amount"> amount of ingredient</param>
        /// <param name="ingredientUnit">unit of ingredient</param>
        public Ingredient(string ingredientName, string amount, string ingredientUnit)
        {
            this.Recipes = new List<Recipe>();
            //this.recs = new ObservableCollection<Recipe>();
            this.IngredientName = ingredientName;
            this.Amount = amount;
            this.id = -1;
            this.IngredientUnit = ingredientUnit;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ingredient"/> class.
        /// </summary>
        /// <param name="ingredientName"> name of ingredient</param>
        /// <param name="amount"> amount of ingredient</param>
        /// <param name="id">id of ingredient</param>
        public Ingredient(string ingredientName, string amount, int id)
        {
            this.Recipes = new List<Recipe>();
            //this.recs = new ObservableCollection<Recipe>();
            this.ingredientName = ingredientName;
            this.amount = amount;
            this.id = id;
        }

       

        /// <summary>
        /// Initializes a new instance of the <see cref="Ingredient"/> class.
        /// </summary>
        /// <param name="ingredientName"> name of ingredient</param>
        /// <param name="amount"> amount of ingredient</param>
        /// <param name="id">id of ingredient</param>
        public Ingredient(string ingredientName)
        {
            this.Recipes = new List<Recipe>();
            //this.recs = new ObservableCollection<Recipe>();
            this.ingredientName = ingredientName;
            this.amount = " ";
            this.id = 1;
        }
        /// <summary>
        /// Initializes a new Instance of the <see cref="Ingredient"/> class.
        /// </summary>
        /// <param name="ingredientName"> name of ingredient</param>
        /// <param name="id">id of ingredient</param>
        public Ingredient(int id, string ingredientName)
        {
            this.Recipes = new List<Recipe>();
            // this.recs = new ObservableCollection<Recipe>();
            this.ingredientName = ingredientName;
            this.amount = " ";
            this.id = id;
        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------
        /// <summary>
        /// Gets or sets the id of the student.
        /// </summary>
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.OnPropertyChanged(nameof(this.Id));
                   
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the ingredient.
        /// </summary>
        public string IngredientName
        {
            get
            {
               
                return this.ingredientName;
                
            }

            set
            {
                if (this.ingredientName != value)
                {
                    this.ingredientName = value;
                    this.OnPropertyChanged(nameof(this.ingredientName));
                   
                    
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the ingredient.
        /// </summary>
        public string IngredientUnit
        {
            get
            {
                return this.ingredientUnit;
            }

            set
            {
                if (this.ingredientUnit != value)
                {
                    this.ingredientUnit = value;
                    this.OnPropertyChanged(nameof(this.ingredientUnit));

                   
                }
            }
        }

        /// <summary>
        /// Gets or sets the amount of the ingredient.
        /// </summary>
        public string Amount
        {
            get
            {
                return this.amount;
            }

            set
            {
                if (this.amount != value)
                {
                    this.amount = value;
                    this.OnPropertyChanged(nameof(this.amount));
                   
                    
                }
            }
        }


        public virtual ICollection<Recipe> Recipes { get; set; }

        #endregion

        /// <summary>
        /// Called, when Ingredient data is saved.
        /// </summary>
        public void OnIngredientDataChanged()
        {
            // Save data.
          
        }
    }
}
