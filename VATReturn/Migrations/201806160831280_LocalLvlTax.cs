namespace VATReturn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocalLvlTax : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocalLvlTaxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(),
                        VatRegNo = c.String(),
                        Blink = c.Boolean(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Vat = c.Double(nullable: false),
                        InstitutionInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InstitutionInfoes", t => t.InstitutionInfoId, cascadeDelete: true)
                .Index(t => t.InstitutionInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocalLvlTaxes", "InstitutionInfoId", "dbo.InstitutionInfoes");
            DropIndex("dbo.LocalLvlTaxes", new[] { "InstitutionInfoId" });
            DropTable("dbo.LocalLvlTaxes");
        }
    }
}
