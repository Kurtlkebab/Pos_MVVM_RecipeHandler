﻿using MVVM_RecipeHandler_Common.NotifyPropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace MVVM_RecipeHandler_Models.DataClasses
{ /// <summary>
  /// Derives from the <see cref="NotifyPropertyChanged"/> class.
  /// </summary>
    public class Recipe : NotifyPropertyChanged
    {
        #region ------------- Fields, Constants, Delegates ------------------------

        private ObservableCollection<Ingredient> ingredients;

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
        private int id;

        private string pictureURL;

        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        /// <param name="recipeDescription">short description of the recipe..</param>
        /// <param name="recipeName">name of the recipe.</param>
        public Recipe()
        {
            this.Ingredients = new ObservableCollection<Ingredient>();
           
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        /// <param name="recipeDescription">short description of the recipe..</param>
        /// <param name="recipeName">name of the recipe.</param>
        public Recipe(string recipeName, string recipeDescription)
        {
            this.Ingredients = new ObservableCollection<Ingredient>();
            this.recipeName = recipeName;
            this.recipeDescription= recipeDescription;
            this.id = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        /// <param name="recipeDescription">short description of the recipe..</param>
        /// <param name="recipeName">name of the recipe.</param>
        /// <param name="id">id of the recipe.</param>
        public Recipe(string recipeName, string recipeDescription, int id, string pictureUrl)
        {
            this.Ingredients = new ObservableCollection<Ingredient>();
            this.recipeName = recipeName;
            this.recipeDescription = recipeDescription;
            this.id = id;
            this.pictureURL = pictureUrl;
        }

        #endregion

        #region ------------- Properties, Indexer ---------------------------------
        /// <summary>
        /// Gets or sets the list with all Ingredient data.
        /// </summary>
        public  ObservableCollection<Ingredient> Ingredients { get; set; }
        public virtual Ingredient Ingredientss { get; set; }

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
                    this.StudentChanged = true;
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
                    this.StudentChanged = true;
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
                  //  this.pictureURL= BuildImgString(value);
                    this.OnPropertyChanged(nameof(this.pictureURL));
                    this.StudentChanged = true;
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

        public string ImageToBase64String(Image image, ImageFormat format)
        {
            MemoryStream memory = new MemoryStream();
            image.Save(memory, format);
            string base64 = Convert.ToBase64String(memory.ToArray());
            memory.Close();

            return base64;
        }

        public Image ImageFromBase64String(string base64)
        {
            MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64));
            Image result = Image.FromStream(memory);
            memory.Close();

            return result;
        }
       
        public string BuildImgString(string path)
        {

            Image img = Image.FromFile(path);
            string ImageString = ImageToBase64String(img, ImageFormat.Jpeg);
            return ImageString;
        }

    }
}
