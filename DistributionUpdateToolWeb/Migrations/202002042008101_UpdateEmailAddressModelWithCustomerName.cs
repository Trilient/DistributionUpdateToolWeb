namespace DistributionUpdateToolWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmailAddressModelWithCustomerName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailAddresses", "CustomerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailAddresses", "CustomerName");
        }
    }
}
