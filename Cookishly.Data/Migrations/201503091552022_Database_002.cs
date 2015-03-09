namespace Cookishly.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_002 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IngredientEntities", newName: "Ingredients");
            RenameTable(name: "dbo.IngredientSpecificationEntities", newName: "IngredientSpecifications");
            RenameTable(name: "dbo.RecipeEntities", newName: "Recipes");
            RenameTable(name: "dbo.ProfileEntities", newName: "Profiles");
            RenameTable(name: "dbo.RecipeStepEntities", newName: "RecipeSteps");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.RecipeSteps", newName: "RecipeStepEntities");
            RenameTable(name: "dbo.Profiles", newName: "ProfileEntities");
            RenameTable(name: "dbo.Recipes", newName: "RecipeEntities");
            RenameTable(name: "dbo.IngredientSpecifications", newName: "IngredientSpecificationEntities");
            RenameTable(name: "dbo.Ingredients", newName: "IngredientEntities");
        }
    }
}
