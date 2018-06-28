namespace VATReturn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLocalLvlTax : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LocalLvlTaxes", "SystemDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ValueAddedTaxes", "DateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ValueAddedTaxes", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.LocalLvlTaxes", "SystemDate");
        }
    }
}
