namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tb_Product", "selectedProductCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Product", "selectedProductCategory", c => c.Int(nullable: false));
        }
    }
}
