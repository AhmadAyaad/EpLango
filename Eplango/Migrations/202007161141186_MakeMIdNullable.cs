namespace Eplango.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeMIdNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departments", "ManagerId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departments", "ManagerId", c => c.Int(nullable: false));
        }
    }
}
