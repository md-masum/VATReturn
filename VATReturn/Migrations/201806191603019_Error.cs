namespace VATReturn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Error : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RebateExports", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RebateExports", "DateTime");
        }
    }
}
