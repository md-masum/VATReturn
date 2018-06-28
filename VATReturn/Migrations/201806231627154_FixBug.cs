namespace VATReturn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixBug : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LocalLvlTaxes", "SystemDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LocalLvlTaxes", "SystemDate", c => c.DateTime(nullable: false));
        }
    }
}
