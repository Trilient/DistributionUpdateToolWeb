namespace DistributionUpdateToolWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedEmailAddressData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO EmailAddresses (ClientId, Email) VALUES (1, 'cap@vitol.com')");
            Sql("INSERT INTO EmailAddresses (ClientId, Email) VALUES (1, 'ops@vitol.com')");
            Sql("INSERT INTO EmailAddresses (ClientId, Email) VALUES (2, 'Sherry@p66.com')");
            Sql("INSERT INTO EmailAddresses (ClientId, Email) VALUES (2, 'RyanH@p66.com')");
            Sql("INSERT INTO EmailAddresses (ClientId, Email) VALUES (2, 'lpgops@p66.com')");
            Sql("INSERT INTO EmailAddresses (ClientId, Email) VALUES (2, 'vessels@p66.com')");
            Sql("INSERT INTO EmailAddresses (ClientId, Email) VALUES (3, 'jgarciaherrera@repsol.com')");
            Sql("INSERT INTO EmailAddresses (ClientId, Email) VALUES (3, 'lpg.ops@repsol.com')");
            Sql("INSERT INTO EmailAddresses (ClientId, Email) VALUES (4, 'christa.squiteri@glencore.us')");
            Sql("INSERT INTO EmailAddresses (ClientId, Email) VALUES (4, 'operations@glencore.co.uk')");
            Sql("INSERT INTO EmailAddresses (ClientId, Email) VALUES (4, 'greg@glencore.co.uk')");
        }
        
        public override void Down()
        {
        }
    }
}
