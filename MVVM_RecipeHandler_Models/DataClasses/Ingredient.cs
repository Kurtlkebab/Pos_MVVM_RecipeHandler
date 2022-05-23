using MVVM_RecipeHandler_Common.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        // public virtual ICollection<Recipe> recipe { get; set; }

        //public Recipe Recipe { get; set; }
        /// <summary>
        /// Id of the Ingredient.
        /// </summary>
        private int id;

        private string unit;

        public virtual ICollection<Recipe> recs { get; set; }
        public  string Unit { get; set; }
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
            this.recs = new ObservableCollection<Recipe>();
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
            this.recs = new ObservableCollection<Recipe>();
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
            this.recs = new ObservableCollection<Recipe>();
            this.ingredientName = ingredientName;
            this.amount = " ";
            this.id = -1;
        }
        /// <summary>
        /// Initializes a new Instance of the <see cref="Ingredient"/> class.
        /// </summary>
        /// <param name="ingredientName"> name of ingredient</param>
        /// <param name="id">id of ingredient</param>
        public Ingredient(int id, string ingredientName)
        {
            this.recs = new ObservableCollection<Recipe>();
            this.ingredientName = ingredientName;
            this.amount = " ";
            this.id = id;
        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------
        /// <summary>
        /// Gets or sets the id of the student.
        /// </summary>
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
               // asdsasdsad
                return this.ingredientName;
                //this iis a comment
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

        

        
        #endregion

        /// <summary>
        /// Called, when student data is saved.
        /// </summary>
        public void OnStudentDataSaved()
        {
            // Save data.
          
        }
    }
}
