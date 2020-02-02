namespace DistributionUpdateToolWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedClientData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Clients (Name) VALUES ('Vitol')");
            Sql("INSERT INTO Clients (Name) VALUES ('Phillips 66')");
            Sql("INSERT INTO Clients (Name) VALUES ('Repsol')");
            Sql("INSERT INTO Clients (Name) VALUES ('Glencore')");
        }
        
        public override void Down()
        {
        }
    }
}
