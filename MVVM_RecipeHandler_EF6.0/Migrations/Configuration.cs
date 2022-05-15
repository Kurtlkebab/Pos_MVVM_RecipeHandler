namespace MVVM_RecipeHandler_EF6._0.Migrations
{
    using MVVM_RecipeHandler_Models.DataClasses;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    internal sealed class Configuration : DbMigrationsConfiguration<MVVM_RecipeHandler_EF6._0.RecipeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVVM_RecipeHandler_EF6._0.RecipeContext context)
        {
            //if (context.RecipesSet.Any())
            //{
            //    return;
            //}




            string ImageString = BuildImgString("../../MVVM_RecipeHandler/Images/Gnochi.jpg");
            List<Recipe> RecipeSeed = new List<Recipe>();
            RecipeSeed.Add(new Recipe("Trüffelgnochi", "Schlagobers auf mittlerer Hitze auf die Hälfte einreduzieren mit halbem Rindssuppenwürfel Salz und Pfeffer abschmecken. Gnochi nach Anleitung kochen mit Sauce übergießen und mit bevorzugter Menge Trüffelöl abschmecken",1, ImageString) );
           // RecipeSeed.Add(new Recipe("Trüffelgnochi2", "Schlagobers auf mittlerer Hitze auf die Hälfte einreduzieren mit halbem Rindssuppenwürfel Salz und Pfeffer abschmecken. Gnochi nach Anleitung kochen mit Sauce übergießen und mit bevorzugter Menge Trüffelöl abschmecken",2," this is text") );
            //RecipeSeed.Add(new Recipe("Trüffelgnochi3", "Schlagobers auf mittlerer Hitze auf die Hälfte einreduzieren mit halbem Rindssuppenwürfel Salz und Pfeffer abschmecken. Gnochi nach Anleitung kochen mit Sauce übergießen und mit bevorzugter Menge Trüffelöl abschmecken",3," this is text") );
            context.RecipesSet.AddRange(RecipeSeed);
            // execute sql command
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
        public string ImageToBase64String(Image image, ImageFormat format)
        {
            MemoryStream memory = new MemoryStream();
            image.Save(memory, format);
            string base64 = Convert.ToBase64String(memory.ToArray());
            memory.Close();

            return base64;
        }
        public string BuildImgString(string path)
        {

            Image img = Image.FromFile(path);
            string ImageString = ImageToBase64String(img, ImageFormat.Jpeg);
            return ImageString;
        }
    }
}
