namespace MVVM_RecipeHandler_EF6._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Units", "StudentChanged");
            DropForeignKey("dbo.Ingredients", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.Ingredients", new[] { "Recipe_Id" });
            DropColumn("dbo.Ingredients", "Recipe_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Units", "StudentChanged", c => c.Boolean(nullable: false));
        }
    }
}
