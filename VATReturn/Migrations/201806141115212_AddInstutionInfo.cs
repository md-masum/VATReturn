namespace VATReturn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInstutionInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InstitutionInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaxId = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 30),
                        ActivitiesArea = c.String(),
                        AreaCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InstitutionInfoes");
        }
    }
}
