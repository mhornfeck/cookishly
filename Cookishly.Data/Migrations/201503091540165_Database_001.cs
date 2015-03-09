namespace Cookishly.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IngredientEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageUrl = c.String(),
                        Category = c.Int(nullable: false),
                        ProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProfileEntities", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.IngredientSpecificationEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.String(),
                        Units = c.String(),
                        Preparation = c.String(),
                        IngredientId = c.Int(nullable: false),
                        RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RecipeEntities", t => t.RecipeId, cascadeDelete: true)
                .ForeignKey("dbo.IngredientEntities", t => t.IngredientId, cascadeDelete: true)
                .Index(t => t.IngredientId)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.RecipeEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageUrl = c.String(),
                        Category = c.Int(nullable: false),
                        ProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProfileEntities", t => t.ProfileId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.ProfileEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RecipeStepEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ordinal = c.Int(nullable: false),
                        Description = c.String(),
                        RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RecipeEntities", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ProfileEntityApplicationUsers",
                c => new
                    {
                        ProfileEntity_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ProfileEntity_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.ProfileEntities", t => t.ProfileEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.ProfileEntity_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.IngredientSpecificationEntities", "IngredientId", "dbo.IngredientEntities");
            DropForeignKey("dbo.RecipeStepEntities", "RecipeId", "dbo.RecipeEntities");
            DropForeignKey("dbo.ProfileEntityApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProfileEntityApplicationUsers", "ProfileEntity_Id", "dbo.ProfileEntities");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RecipeEntities", "ProfileId", "dbo.ProfileEntities");
            DropForeignKey("dbo.IngredientEntities", "ProfileId", "dbo.ProfileEntities");
            DropForeignKey("dbo.IngredientSpecificationEntities", "RecipeId", "dbo.RecipeEntities");
            DropIndex("dbo.ProfileEntityApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ProfileEntityApplicationUsers", new[] { "ProfileEntity_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RecipeStepEntities", new[] { "RecipeId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.RecipeEntities", new[] { "ProfileId" });
            DropIndex("dbo.IngredientSpecificationEntities", new[] { "RecipeId" });
            DropIndex("dbo.IngredientSpecificationEntities", new[] { "IngredientId" });
            DropIndex("dbo.IngredientEntities", new[] { "ProfileId" });
            DropTable("dbo.ProfileEntityApplicationUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RecipeStepEntities");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ProfileEntities");
            DropTable("dbo.RecipeEntities");
            DropTable("dbo.IngredientSpecificationEntities");
            DropTable("dbo.IngredientEntities");
        }
    }
}
