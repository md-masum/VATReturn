namespace VATReturn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValueAddedTax : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ValueAddedTaxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ZeroReturns = c.Boolean(nullable: false),
                        Percentage = c.Int(nullable: false),
                        TaxableGoodsSalePrice = c.Double(nullable: false),
                        TaxableGoodsSupplementaryDuty = c.Double(),
                        TaxableGoodsValueAddedTax = c.Double(),
                        ZeroRatedSalePrice = c.Double(),
                        ZeroRatedSupplementaryDuty = c.Double(),
                        ZeroRatedValueAddedTax = c.Double(),
                        ExemptSalePrice = c.Double(),
                        ExemptSupplementaryDuty = c.Double(),
                        ExemptValueAddedTax = c.Double(),
                        TotalTaxPayable = c.Double(),
                        SourceCut = c.Double(),
                        Owing = c.Double(),
                        Amercement = c.Double(),
                        Fine = c.Double(),
                        RentSppace = c.Double(),
                        OtherConsolidation = c.Double(),
                        TotalPayable = c.Double(),
                        LocalLvlTaxPurchasePrise = c.Double(),
                        LocalLvlTaxTaxableTax = c.Double(),
                        ImportPurchasePrise = c.Double(),
                        ImportTaxableTax = c.Double(),
                        RebateExportPrise = c.Double(),
                        RebateExportTax = c.Double(),
                        ExemptProductsPrise = c.Double(),
                        TotalRebateTax = c.Double(),
                        OtherCoordination = c.Double(),
                        PreviousMonthOdds = c.Double(),
                        TotalRevenue = c.Double(),
                        NetPayable = c.Double(),
                        DepositedTreasury = c.Double(),
                        NextMonth = c.Double(),
                        RefugeesDirectorate = c.Double(),
                        TotalGrocersDischarged = c.Double(),
                        Bank = c.String(nullable: false),
                        Branch = c.String(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        InstitutionInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InstitutionInfoes", t => t.InstitutionInfoId, cascadeDelete: true)
                .Index(t => t.InstitutionInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValueAddedTaxes", "InstitutionInfoId", "dbo.InstitutionInfoes");
            DropIndex("dbo.ValueAddedTaxes", new[] { "InstitutionInfoId" });
            DropTable("dbo.ValueAddedTaxes");
        }
    }
}
