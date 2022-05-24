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
    using System.Collections.ObjectModel;

    internal sealed class Configuration : DbMigrationsConfiguration<MVVM_RecipeHandler_EF6._0.RecipeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVVM_RecipeHandler_EF6._0.RecipeContext context)
        {
            //if (context.RecipesSet.Any() && context.UnitsSet.Any() && context.IngredientsSet.Any())
            //{
            //    return;
            //}


            try
            {
                string ImageString = BuildImgString("../../MVVM_RecipeHandler/Images/Springrolls_cut.jpg");
                string ImageString1 = BuildImgString("../../MVVM_RecipeHandler/Images/double_cheese.jpg");
                string ImageString2 = BuildImgString("../../MVVM_RecipeHandler/Images/curry_roux_cut.jpg");
                string ImageString3 = BuildImgString("../../MVVM_RecipeHandler/Images/eierspeis.jpg");
                string ImageString4 = BuildImgString("../../MVVM_RecipeHandler/Images/Palatschinken.jpg");
                string ImageString5 = BuildImgString("../../MVVM_RecipeHandler/Images/Jap_curry.jpg");
                string ImageString6 = BuildImgString("../../MVVM_RecipeHandler/Images/sesam_dip.jpg");
                string ImageString7 = BuildImgString("../../MVVM_RecipeHandler/Images/yuxiang_eggplant_cut.jpg");
                string ImageString8 = BuildImgString("../../MVVM_RecipeHandler/Images/Gnochi.jpg");






                List<Unit> UnitSeed = new List<Unit>();
                UnitSeed.Add(new Unit("kg"));
                UnitSeed.Add(new Unit("dag"));
                UnitSeed.Add(new Unit("g"));
                UnitSeed.Add(new Unit("Stück"));
                UnitSeed.Add(new Unit("EL"));
                UnitSeed.Add(new Unit("TL"));

                context.UnitsSet.AddRange(UnitSeed);

                List<Ingredient> IngredientSeed = new List<Ingredient>();
                IngredientSeed.Add(new Ingredient("Schlagobers"));
                IngredientSeed.Add(new Ingredient("Mehl"));
                IngredientSeed.Add(new Ingredient("Eier"));
                IngredientSeed.Add(new Ingredient("Zucker"));
                IngredientSeed.Add(new Ingredient("Öl"));
                IngredientSeed.Add(new Ingredient("Schnittlauch"));
                IngredientSeed.Add(new Ingredient("Marmelade"));
                IngredientSeed.Add(new Ingredient("Suppenwürfel"));
                IngredientSeed.Add(new Ingredient("Trüffelöl"));
                IngredientSeed.Add(new Ingredient("Salz"));
                IngredientSeed.Add(new Ingredient("Pfeffer"));
                IngredientSeed.Add(new Ingredient("Milch"));
                IngredientSeed.Add(new Ingredient("Chinkiang Essig"));
                IngredientSeed.Add(new Ingredient("Soja Sauce Hell"));
                IngredientSeed.Add(new Ingredient("Doubanjiang"));
                IngredientSeed.Add(new Ingredient("Knoblauch"));
                IngredientSeed.Add(new Ingredient("Jungzwiebel"));
                IngredientSeed.Add(new Ingredient("Ingwer"));
                IngredientSeed.Add(new Ingredient("Sesam Öl"));
                IngredientSeed.Add(new Ingredient("Hühnersuppe"));
                IngredientSeed.Add(new Ingredient("Auberginen"));
                IngredientSeed.Add(new Ingredient("Tomaten"));
                IngredientSeed.Add(new Ingredient("Erdbeeren"));
                IngredientSeed.Add(new Ingredient("Stärke"));
                IngredientSeed.Add(new Ingredient("Kartoffeln"));
                IngredientSeed.Add(new Ingredient("Apfel"));
                IngredientSeed.Add(new Ingredient("Curry Roux"));
                IngredientSeed.Add(new Ingredient("Ketchup"));
                IngredientSeed.Add(new Ingredient("Karotten"));
                IngredientSeed.Add(new Ingredient("Schwarzer Pfeffer"));
                IngredientSeed.Add(new Ingredient("Zwiebel"));
                IngredientSeed.Add(new Ingredient("Gurke"));
                IngredientSeed.Add(new Ingredient("Salat"));
                IngredientSeed.Add(new Ingredient("Honig"));
                IngredientSeed.Add(new Ingredient("Sake"));
                IngredientSeed.Add(new Ingredient("Hühnerbrust"));
                IngredientSeed.Add(new Ingredient("Hühnerfleisch ausgelöst"));
                IngredientSeed.Add(new Ingredient("Sesamsamen"));
                IngredientSeed.Add(new Ingredient("Gurkerl"));
                IngredientSeed.Add(new Ingredient("Hamburgerspeck"));
                IngredientSeed.Add(new Ingredient("Mayonnaise"));
                IngredientSeed.Add(new Ingredient("Burgerbun"));
                IngredientSeed.Add(new Ingredient("Reispapier"));
                IngredientSeed.Add(new Ingredient("Paprika edelsüß"));
                IngredientSeed.Add(new Ingredient("Zimt"));
                IngredientSeed.Add(new Ingredient("Birne"));
                IngredientSeed.Add(new Ingredient("Rinderfaschiertes"));
                IngredientSeed.Add(new Ingredient("Butter"));
                IngredientSeed.Add(new Ingredient("Chili Öl"));
                IngredientSeed.Add(new Ingredient("Minze"));
                IngredientSeed.Add(new Ingredient("Koriander"));
                IngredientSeed.Add(new Ingredient("Reisessig"));
                IngredientSeed.Add(new Ingredient("Cheddar"));
                IngredientSeed.Add(new Ingredient("Parmesan"));
                IngredientSeed.Add(new Ingredient("Käse"));
                IngredientSeed.Add(new Ingredient("Ingwer"));

                context.IngredientsSet.AddRange(IngredientSeed);

                List<Ingredient> IngredientsTruffle = new List<Ingredient>();


                IngredientsTruffle.Add(IngredientSeed[1].CopyIngredient("g", "250"));
                IngredientsTruffle.Add(IngredientSeed[7].CopyIngredient("Stück", "1/2 nach Geschmack"));
                IngredientsTruffle.Add(IngredientSeed[8].CopyIngredient("ml", "nach Geschmack"));
                IngredientsTruffle.Add(IngredientSeed[9].CopyIngredient("tl", "halber"));
                IngredientsTruffle.Add(IngredientSeed[10].CopyIngredient("tl", "viertel"));


                List<Ingredient> IngredientsEierspeis = new List<Ingredient>();


                IngredientsEierspeis.Add(IngredientSeed[9].CopyIngredient("tl", "halber"));
                IngredientsEierspeis.Add(IngredientSeed[10].CopyIngredient("tl", "viertel"));
                IngredientsEierspeis.Add(IngredientSeed[2].CopyIngredient("Stück", "3"));

                List<Ingredient> IngredientsSpringRolls = new List<Ingredient>();

                IngredientsSpringRolls.Add(new Ingredient("Hähnchenfleisch", "halber", "kg"));
                IngredientsSpringRolls.Add(new Ingredient("Reispapier", "ausreichend", "ausreichend"));
                IngredientsSpringRolls.Add(new Ingredient("Ingwer", "2", "El"));
                IngredientsSpringRolls.Add(new Ingredient("Jungzwiebel", "ausreichend", "ausreichend"));
                IngredientsSpringRolls.Add(new Ingredient("Salat", "ausreichend", "ausreichend"));
                IngredientsSpringRolls.Add(new Ingredient("Sake", "2", "El"));

                List<Ingredient> IngredientsCurryRoux = new List<Ingredient>();

                IngredientsCurryRoux.Add(new Ingredient("Butter", "7", "El"));
                IngredientsCurryRoux.Add(new Ingredient("Mehl", "7", "El"));
                IngredientsCurryRoux.Add(new Ingredient("Curry Pulver", "4", "El"));
                IngredientsCurryRoux.Add(new Ingredient("Garam Masala", "1", "El"));
                IngredientsCurryRoux.Add(new Ingredient("Cayenne Pfeffer", "halber", "tl"));

                List<Ingredient> IngredientsCheeseb = new List<Ingredient>();

                IngredientsCheeseb.Add(new Ingredient("Rinderfaschiertes", "500", "g"));
                IngredientsCheeseb.Add(new Ingredient("Burgerbuns", "4", "Stück"));
                IngredientsCheeseb.Add(new Ingredient("Gurkerl", "ausreichend", "Stück"));
                IngredientsCheeseb.Add(new Ingredient("Zwiebel", "ausreichend", "Stück"));
                IngredientsCheeseb.Add(new Ingredient("Tomaten", "ausreichend", "Stück"));
                IngredientsCheeseb.Add(new Ingredient("Cheddar", "ausreichend", "Stück"));

                List<Ingredient> IngredientsSesamDip = new List<Ingredient>();

                IngredientsSesamDip.Add(new Ingredient("Sesam", "2", "El"));
                IngredientsSesamDip.Add(new Ingredient("Ingwer", "1", "El"));
                IngredientsSesamDip.Add(new Ingredient("Knoblauchzehen", "1", "Stück"));
                IngredientsSesamDip.Add(new Ingredient("Zucker", "3", "EL"));
                IngredientsSesamDip.Add(new Ingredient("Sojasauce", "3", "EL"));
                IngredientsSesamDip.Add(new Ingredient("Reisessig", "2", "EL"));
                IngredientsSesamDip.Add(new Ingredient("Geröstetes Sesamöl", "2", "tl"));
                IngredientsSesamDip.Add(new Ingredient("Chiliöl", "1", "tl"));

                List<Ingredient> IngredientsJapCurry = new List<Ingredient>();
                IngredientsJapCurry.Add(new Ingredient("Hühnchenfleisch", "750", "g"));
                IngredientsJapCurry.Add(new Ingredient("Salz", "viertel", "tl"));
                IngredientsJapCurry.Add(new Ingredient("Karotten", "2", "Stück"));
                IngredientsJapCurry.Add(new Ingredient("Zwiebel", "2", "Stück"));
                IngredientsJapCurry.Add(new Ingredient("Knoblauchzehen", "2", "Stück"));
                IngredientsJapCurry.Add(new Ingredient("Erdäpfel", "2", "Stück"));
                IngredientsJapCurry.Add(new Ingredient("Apfel", "halber", "Stück"));

                List<Ingredient> IngredientsPalatschinken = new List<Ingredient>();
                IngredientsPalatschinken.Add(new Ingredient("Mehl", "250", "g"));
                IngredientsPalatschinken.Add(new Ingredient("Eier", "2", "Stück"));
                IngredientsPalatschinken.Add(new Ingredient("Milch", "500", "ml"));

                List<Ingredient> IngredientsYuXiang = new List<Ingredient>();
                IngredientsYuXiang.Add(new Ingredient("Auberginen", "2", "Stück"));
                IngredientsYuXiang.Add(new Ingredient("Schweinefaschiertes", "150", "g"));
                IngredientsYuXiang.Add(new Ingredient("Doubanjiang", "nach Geschmack", "El"));
                IngredientsYuXiang.Add(new Ingredient("Öl", "5", "El"));
                IngredientsYuXiang.Add(new Ingredient("Zucker", "3", "El"));
                IngredientsYuXiang.Add(new Ingredient("Schwarzer Essig", "3", "El"));
                IngredientsYuXiang.Add(new Ingredient("Jungzwiebel", "3", "Stück"));


                List<Recipe> RecipeSeed = new List<Recipe>();

                Recipe rec2 = new Recipe("Trüffelgnochi", "Schlagobers auf mittlerer Hitze auf die Hälfte einreduzieren mit halbem Rindssuppenwürfel Salz und Pfeffer abschmecken. Gnochi nach Anleitung kochen mit Sauce übergießen und mit bevorzugter Menge Trüffelöl abschmecken", ImageString8, IngredientsTruffle);
                Recipe rec3 = new Recipe("Eierspeis", "Pfanne auf mittlere bis hohe Hitze aufdrehen, Öl hinzufügen. Eier aufschlagen und mit einer Gabel verquirlen. Bis zum gewünschten Gargrad kochen (10 Sekunden bis 1 Mintue) und mit Salz Pfeffer und Schnittlauch servieren", ImageString3, IngredientsEierspeis);
                Recipe rec4 = new Recipe("Chicken Spring Rolls", "Chop the scallions into 3 pieces. Slice 1” ginger into thin slices.Butterfly the chicken breast (See Note 2).First remove the tender (small inner fillet) by cutting away any connective tissue. Turn the breast over and with the edge of a knife parallel to the cutting board,cut the length of the side of the breast. Carefully slice the breast in half widthwise almost to the other edge. Keep the edge intact and open the breast along the fold.In a large pot,add water, 1 tsp. salt and 2 Tbsp. sake that is enough to cover the chicken. Put sliced ginger and scallions and bring it to a boil. Once boiling, put the chicken(breast and tender) in the pot, reduce the heat to the lowest setting, and cover the lid. Cook the chicken for 15 minutes.Meanwhile, juliene carrot and cucumber. I use a juliene slicer but if you don’t have one, cut into thin sheets first and then cut into matchstick size strips.Take out the chicken from the pot onto a plate and immediately cover with the plastic wrap to prevent from drying. Once the chicken has cooled, shred the chicken into small pices with your hands.Rinse the butter lettuce, mint, and cilantro under water and dry completely. Set all the ingredients on the working surface.Pour hot water in a large bowl. Workig with 1 wrapper at a time, soak the wrapper in the hot water and rotate 2-3 times, about 10 seconds. The wrapper will still feel slightly stiff but don’t worry, it will become softer. Place it flat on a working surface.Lay thebutter lettuce, chicken, carrot, and cucumber at bottom 1/3 of the wrapper closest to you, and place the mint and cilantro leaves at the top 1/3 of the wrapper.Pressing firmly down to hold the filling in place, foldthe bottom of the wrapper and start rolling tightly.Around the middle of the wrapper, fold both sides ofthe wrapper in and roll up tightly to the top. Place on a plate and cover loosely with plastic wrapand repeat with the remaining wrappers and fillings. Cut in half or in thirds, and serve with the Sesame Dipping Sauce.If not serving immediately, keep the spring rolls tightly covered with plastic wrap at room temperature for up to 2 hours.", ImageString, IngredientsSpringRolls);
                Recipe rec5 = new Recipe("Japanische Curry Roux", "In a small saucepan, melt the butter over low heat.When the butter is completely melted, add the flour. Stir to combine the butter and flour.Soon the butter and flour fuse and swell. Keep stirring because the roux will easily burn. Cook for 15-20 minutes on low heat. The roux will become light brown color.Add the garam masala, curry powder, and cayenne pepperCook and stir for 30 seconds and remove from the heat. Use the curry roux in your curry recipe. Make sure to taste and season with salt after you add the roux to the dish (as the roux is not salted). This recipe yields 1/3 cup roux; enough for your curry recipe that requires 4 cups liquid. If youre not sure, make double portion as everyone prefers different consistency for curry and you may like it thicker (requires more curry roux).If you do not use it immediately, let it cool in an airtight container with lid and store in the refrigerator for a month or freezer for 3-4 months.", ImageString2, IngredientsCurryRoux);
                Recipe rec6 = new Recipe("Klassischer Double Cheeseburger", "Pro Pattie 125 g Hackfleisch zu einer Kugel formen. Diese Hackfleischkugeln kommen dann auf die Gussplatte und werden mit 1-2 Esslöffel fein geschnittenen Zwiebeln gesmashed . (Je nachdem wie dick werden sie ca. 2-3 Minuten gegrillt, dann gedreht und gewürzt, anschließend direkt mit dem Cheddar belegt damit dieser leicht schmelzen kann und schön verläuft. Den Bacon gleichzeitig mit den Patties grillen und auf den geschmolzenen Cheddar legen.Die Buns durchschneiden und von den Innenseiten leicht anrösten.Nun die untere Bunhälfte mit Ketchup, Salat, evtl.karamellisierte Zwiebel und das erste Pattie mit dem Cheddar und Bacon belegen, sowie die Zwiebelringe.Anschließend das zweite Pattie mit Cheddar und Tomatenscheibe, Zwiebelringen und Mayonnaise, zum Schluss die obere Bunhälfte auflegen.", ImageString5, IngredientsCheeseb);
                Recipe rec7 = new Recipe("Sesam Dipping Sauce", "For the sauce, grind 2 Tbsp. toasted sesame seeds in your mortar and pestle. Transfer the ground sesame seeds into a small bowl. Then grate the ½” ginger and 1-2 garlic cloves (I used Micropane grater).Add 3 Tbsp. sugar, 3 Tbsp. soy sauce, 2 Tbsp. rice vinegar, 2 tsp. sesame oil, and 1 tsp. la-yu in the bowl and mix well. Set aside.", ImageString6, IngredientsSesamDip);
                Recipe rec8 = new Recipe("Japanisches Curry", "Discard the extra fat from the chicken and cut it into bite size pieces. Season with a little bit of salt and pepper.Peel and cut the carrot in rolling wedges (Rangiri) and cut the onions in wedges.Cut the potatoes into 1.5 inch pieces and soak in water for 15 minutes to remove excess starch.Grate the ginger and crush the garlic.Heat the oil in a large pot over medium heat and sauté the onions until they become translucent.Add the ginger and garlic.Add the chicken and cook until the chicken changes color.Add the carrot and mix.Add the chicken broth(or water).Bring the stock to boil and skim the scrum and fat from the surface of the stock.Peel the apple and grate it(use as much as you like to add sweetness)Add the honey and salt and simmer uncovered for 20 minutes, stirring occasionally.Add the potatoes and cook for 15 minutes, or until the potatoes are tender, and turn off the heat.Meanwhile you can make homemade curry roux.When the potatoes are ready, add the curry.If you use the store - bought curry roux, put 1 - 2 blocks of roux in a ladle and slowly let it dissolve with spoon or chopsticks.Continue with the rest of blocks.Then skip the next Step.If you are using homemade curry roux, add a ladleful or two of cooking liquidfrom the stock and mix into the curry paste.Add more cooking liquid if necessary and mix welluntil it’s smooth.Add the roux paste back into thestock in the large pot and stir to combine.Add soy sauce and ketchup.Simmer uncovered on low heat, stirring occasionally,until the curry becomes thick.Serve the curry with Japanese rice on the side and garnish with soft boiled egg and Fukujinzuke.You can store the curry in the refrigerator up to 2 - 3 days and in the freezer for 1 month.Potatoes will change the texture so you can take  them out before freezing.", ImageString5, IngredientsJapCurry);
                Recipe rec9 = new Recipe("Yu Xiang Aubergine (chinesisch)", "Aubergine in breite Stäbe schneiden mit einer Priese Salz schwenken,zwischen Küchenrollenpapier auflegen,und beschweren(Schwere Bücher oder Teller etc.) und somit Feuchtigkeit für mind. 30 Minuten entziehen.Auberginen bei 175 Grad frittieren bis sie goldbraun sind.Zucker mit Essig und Suppe vermischen und mit Sojasauce abschmecken.Doubanjiang solange im Öl anbraten bis das Öl rot ist, Ingwer, Knoblauch und Jungzwiebel hinzufügen kurz mitbraten die Sauce von vorhin hinzufügen, zum kochen bringen, AUberginen hinzufügen und je nach Geschmack binden(1, 5 tl Kartoffelstärke / 1El Wasser).Servieren mit Reis und klein geschnittenen Jungzwiebel.", ImageString7, IngredientsYuXiang);
                Recipe rec10 = new Recipe("Palatschinken", "Für die Palatschinken zuerst Mehl, Milch, Eier, Salz mit dem Schneebesen in einer Schüessel glatt rühren. Ca. 10 Min. stehen lassen, dadurch wird der Teig etwas dicker und danach nochmals gut durchrühren.Sollte der Palatschinkenteig zu dick sein, mit etwas Mineral oder Soda verdünnen.In einer beschichteten Pfanne einen Schuss Öl erhitzen(das Öl sollte ganz heiß sein, dann gelingt die erste Palatschinke sofort).Dann etwas Teig(mit einem Schöpfer) in die heiße Pfanne hineingegeben.Die Pfanne dabei immer wieder schwenken, sodass der Boden gleichmäßig dünn mit Teig bedeckt ist.Mit dem Pfannenwender die Palatschinke mehrmals wenden und von beiden Seiten goldgelb ausbacken.Die Fertig gebackenen Palatschinken halten sie im Backofen bei ca. 60 Grad warm.Mit beliebigen Zutaten(Marmelade, Nutella, Schinken & Käse etc.) die Palatschinken bestreichen und zusammenrollen.", ImageString4, IngredientsPalatschinken);



                RecipeSeed.Add(rec2);
                RecipeSeed.Add(rec3);
                RecipeSeed.Add(rec4);
                RecipeSeed.Add(rec5);
                RecipeSeed.Add(rec6);
                RecipeSeed.Add(rec7);
                RecipeSeed.Add(rec8);
                RecipeSeed.Add(rec9);
                RecipeSeed.Add(rec10);

                context.RecipesSet.AddRange(RecipeSeed);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
            }

            //  execute sql command
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
