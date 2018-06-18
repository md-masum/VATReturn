namespace VATReturn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImportTaxableGoodses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportTaxableGoods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BandENo = c.String(nullable: false),
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
            DropForeignKey("dbo.ImportTaxableGoods", "InstitutionInfoId", "dbo.InstitutionInfoes");
            DropIndex("dbo.ImportTaxableGoods", new[] { "InstitutionInfoId" });
            DropTable("dbo.ImportTaxableGoods");
        }
    }
}
