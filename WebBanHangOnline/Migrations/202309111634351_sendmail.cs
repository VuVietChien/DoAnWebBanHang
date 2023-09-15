namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sendmail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Order", "Email", c => c.String());
            AlterColumn("dbo.tb_Order", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Order", "Phone", c => c.String());
            AlterColumn("dbo.tb_Order", "Email", c => c.String(nullable: false));
        }
    }
}
