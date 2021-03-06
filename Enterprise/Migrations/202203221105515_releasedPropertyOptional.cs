namespace Enterprise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class releasedPropertyOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Released", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Released", c => c.Boolean(nullable: false));
        }
    }
}
