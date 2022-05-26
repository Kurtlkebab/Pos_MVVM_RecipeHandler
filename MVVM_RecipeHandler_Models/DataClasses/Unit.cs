using MVVM_RecipeHandler_Common.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_RecipeHandler_Models.DataClasses
{
    /// <summary>
    /// Derives from the <see cref="NotifyPropertyChanged"/> class.
    /// </summary>
    public class Unit : NotifyPropertyChanged
    {
        #region ------------- Fields, Constants, Delegates ------------------------

        /// <summary>
        /// Id of the recipe.
        /// </summary>
        private int id;

        /// <summary>
        /// Amount of the Ingredient.
        /// </summary>
        private string unitName;

        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class.
        /// </summary>
        /// <param name="unitname"> amount of ingredient</param>
        public Unit(string unitname)
        {     
            this.unitName = unitname;       
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class.
        /// </summary>
        public Unit()
        {        
        }

        #endregion

        #region ------------- Properties, Indexer ---------------------------------

        /// <summary>
        /// Gets or sets the amount of the ingredient.
        /// </summary>
        public string UnitName
        {
            get
            {
                return this.unitName;
            }

            set
            {
                if (this.unitName != value)
                {
                    this.unitName = value;
                    this.OnPropertyChanged(nameof(this.unitName));                    
                }
            }
        }

        /// <summary>
        /// Gets or sets the id of the recipe.
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
        #endregion
    }
}
