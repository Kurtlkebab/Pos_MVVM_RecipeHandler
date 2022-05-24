using MVVM_RecipeHandler_Common.NotifyPropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVVM_RecipeHandler_Models.DataClasses
{ /// <summary>
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

        /// <summaryr></summaryr>
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
        /// <param name="recipeDescription">short description of the recipe..</param>
        /// <param name="recipeName">name of the recipe.</param>
        public Recipe()
        {
            this.Ingredients = new List<Ingredient>();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        /// <param name="recipeDescription">short description of the recipe..</param>
        /// <param name="recipeName">name of the recipe.</param>
        public Recipe(string recipeName, string recipeDescription, List<Ingredient>ingredients)
        {
            this.Ingredients = new List<Ingredient>();
            this.Ingredients = ingredients;
            //this.Ings = new ObservableCollection<Ingredient>();
            this.recipeName = recipeName;
            this.recipeDescription= recipeDescription;
            //this.recipeid = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        /// <param name="recipeDescription">short description of the recipe..</param>
        /// <param name="recipeName">name of the recipe.</param>
        /// <param name="id">id of the recipe.</param>
        public Recipe(string recipeName, string recipeDescription, string pictureUrl, List<Ingredient> ingredients)
        {
            this.Ingredients = new List<Ingredient>();
            this.Ingredients = ingredients;
            // this.Ings = new ObservableCollection<Ingredient>();
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
        /// Gets or sets the id of the recipe.
        /// </summary>
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
                  //  this.pictureURL= BuildImgString(value);
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

        /// <summary>
        /// Called, when student data is saved.
        /// </summary>
        public void OnStudentDataSaved()
        {
            // Save data.
           
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
