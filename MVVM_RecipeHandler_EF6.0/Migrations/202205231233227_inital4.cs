namespace MVVM_RecipeHandler_EF6._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inital4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IngredientsRecipes",
                c => new
                    {
                        RecipeID = c.Int(nullable: false),
                        IngredientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RecipeID, t.IngredientID })
                .ForeignKey("dbo.Ingredients", t => t.IngredientID, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.RecipeID, cascadeDelete: true)
                .Index(t => t.RecipeID)
                .Index(t => t.IngredientID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngredientsRecipes", "RecipeID", "dbo.Recipes");
            DropForeignKey("dbo.IngredientsRecipes", "IngredientID", "dbo.Ingredients");
            DropIndex("dbo.IngredientsRecipes", new[] { "IngredientID" });
            DropIndex("dbo.IngredientsRecipes", new[] { "RecipeID" });
            DropTable("dbo.IngredientsRecipes");
        }
    }
}
