﻿namespace MVVM_RecipeHandler_EF6._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientName = c.String(),
                        IngredientUnit = c.String(),
                        Amount = c.String(),
                        Recipe_Id = c.Int(),
                        Recipe_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id)
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id1)
                .Index(t => t.Recipe_Id)
                .Index(t => t.Recipe_Id1);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeName = c.String(),
                        PictureURL = c.String(),
                        RecipeDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UnitName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredients", "Recipe_Id1", "dbo.Recipes");
            DropForeignKey("dbo.Ingredients", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.Ingredients", new[] { "Recipe_Id1" });
            DropIndex("dbo.Ingredients", new[] { "Recipe_Id" });
            DropTable("dbo.Units");
            DropTable("dbo.Recipes");
            DropTable("dbo.Ingredients");
        }
    }
}