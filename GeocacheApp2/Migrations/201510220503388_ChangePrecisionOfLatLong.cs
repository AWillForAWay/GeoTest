namespace GeocacheApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePrecisionOfLatLong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Geocaches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Latitude = c.Decimal(nullable: false, precision: 12, scale: 6),
                        Longitude = c.Decimal(nullable: false, precision: 12, scale: 6),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Geocaches");
        }
    }
}
