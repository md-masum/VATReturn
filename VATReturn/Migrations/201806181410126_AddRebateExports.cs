namespace VATReturn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRebateExports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RebateExports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomsDuty = c.Double(),
                        RegulatoryDuties = c.Double(),
                        SupplementaryDuty = c.Double(),
                        InstitutionInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InstitutionInfoes", t => t.InstitutionInfoId, cascadeDelete: true)
                .Index(t => t.InstitutionInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RebateExports", "InstitutionInfoId", "dbo.InstitutionInfoes");
            DropIndex("dbo.RebateExports", new[] { "InstitutionInfoId" });
            DropTable("dbo.RebateExports");
        }
    }
}
