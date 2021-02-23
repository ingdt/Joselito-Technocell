namespace Joselito_Technocell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRemarks : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cities", new[] { "DepartmentId" });
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Users", "Photo", c => c.String(maxLength: 256));
            AlterColumn("dbo.Users", "Phone", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Users", "Address", c => c.String(nullable: false, maxLength: 256));
            CreateIndex("dbo.Categories", "Name", unique: true, name: "CategoryNameIndex");
            CreateIndex("dbo.Categories", "CompanyId", unique: true, name: "CategoryDepartmentIdIndex");
            CreateIndex("dbo.Cities", "Name", unique: true, name: "CityNameIndex");
            CreateIndex("dbo.Cities", "DepartmentId", unique: true, name: "CityDepartmentIdIndex");
            CreateIndex("dbo.Users", "UserName", unique: true, name: "UserEmailIndex");
            CreateIndex("dbo.Users", "CityId");
            AddForeignKey("dbo.Users", "CityId", "dbo.Cities", "CityId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.Users", "UserEmailIndex");
            DropIndex("dbo.Cities", "CityDepartmentIdIndex");
            DropIndex("dbo.Cities", "CityNameIndex");
            DropIndex("dbo.Categories", "CategoryDepartmentIdIndex");
            DropIndex("dbo.Categories", "CategoryNameIndex");
            AlterColumn("dbo.Users", "Address", c => c.String());
            AlterColumn("dbo.Users", "Phone", c => c.String());
            AlterColumn("dbo.Users", "Photo", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            CreateIndex("dbo.Cities", "DepartmentId");
        }
    }
}
