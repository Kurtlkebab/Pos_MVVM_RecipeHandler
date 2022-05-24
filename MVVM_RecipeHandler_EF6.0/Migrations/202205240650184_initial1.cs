namespace MVVM_RecipeHandler_EF6._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ingredients", "Unit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredients", "Unit", c => c.String());
        }
    }
}
