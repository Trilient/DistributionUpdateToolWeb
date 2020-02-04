namespace DistributionUpdateToolWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClientModelWithICollection : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.EmailAddresses", "ClientId");
            AddForeignKey("dbo.EmailAddresses", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailAddresses", "ClientId", "dbo.Clients");
            DropIndex("dbo.EmailAddresses", new[] { "ClientId" });
        }
    }
}
