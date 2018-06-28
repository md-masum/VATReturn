namespace VATReturn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherCoordination : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OtherCoordinations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OtherRebates = c.Double(),
                        Owing = c.Double(),
                        SourceCut = c.Double(),
                        Blank = c.Boolean(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        InstitutionInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InstitutionInfoes", t => t.InstitutionInfoId, cascadeDelete: true)
                .Index(t => t.InstitutionInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OtherCoordinations", "InstitutionInfoId", "dbo.InstitutionInfoes");
            DropIndex("dbo.OtherCoordinations", new[] { "InstitutionInfoId" });
            DropTable("dbo.OtherCoordinations");
        }
    }
}
