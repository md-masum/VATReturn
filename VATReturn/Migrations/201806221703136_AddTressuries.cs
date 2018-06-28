namespace VATReturn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTressuries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Treasuries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TreasuryNo = c.String(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Bank = c.String(nullable: false),
                        Branch = c.String(nullable: false),
                        Quantity = c.Double(nullable: false),
                        InstitutionInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InstitutionInfoes", t => t.InstitutionInfoId, cascadeDelete: true)
                .Index(t => t.InstitutionInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Treasuries", "InstitutionInfoId", "dbo.InstitutionInfoes");
            DropIndex("dbo.Treasuries", new[] { "InstitutionInfoId" });
            DropTable("dbo.Treasuries");
        }
    }
}
