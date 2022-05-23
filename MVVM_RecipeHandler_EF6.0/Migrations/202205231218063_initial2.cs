namespace MVVM_RecipeHandler_EF6._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "Ingredient_Id", c => c.Int());
            CreateIndex("dbo.Recipes", "Ingredient_Id");
            AddForeignKey("dbo.Recipes", "Ingredient_Id", "dbo.Ingredients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "Ingredient_Id", "dbo.Ingredients");
            DropIndex("dbo.Recipes", new[] { "Ingredient_Id" });
            DropColumn("dbo.Recipes", "Ingredient_Id");
        }
    }
}
