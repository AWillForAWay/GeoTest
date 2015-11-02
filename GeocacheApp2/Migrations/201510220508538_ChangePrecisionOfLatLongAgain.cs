namespace GeocacheApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePrecisionOfLatLongAgain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Geocaches", "Latitude", c => c.Decimal(nullable: false, precision: 10, scale: 6));
            AlterColumn("dbo.Geocaches", "Longitude", c => c.Decimal(nullable: false, precision: 10, scale: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Geocaches", "Longitude", c => c.Decimal(nullable: false, precision: 12, scale: 10));
            AlterColumn("dbo.Geocaches", "Latitude", c => c.Decimal(nullable: false, precision: 12, scale: 10));
        }
    }
}
