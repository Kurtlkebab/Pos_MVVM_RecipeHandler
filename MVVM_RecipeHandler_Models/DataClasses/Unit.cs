﻿using MVVM_RecipeHandler_Common.NotifyPropertyChanged;
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
        /// Amount of the Ingredient.
        /// </summary>
        private string unitName;

        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new Instance of the <see cref="Unit"/> class.
        /// </summary>
        /// <param name="unitname"> amount of ingredient</param>
        /// <param name="id">id of ingredient</param>
        public Unit(string unitname)
        {
          
            this.unitName = unitname;
          
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

                    this.StudentChanged = true;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the properties have changed.
        /// </summary>
        public bool StudentChanged { get; private set; }
        #endregion

        /// <summary>
        /// Called, when student data is saved.
        /// </summary>
        public void OnStudentDataSaved()
        {
            // Save data.
            this.StudentChanged = false;
        }
    }
}