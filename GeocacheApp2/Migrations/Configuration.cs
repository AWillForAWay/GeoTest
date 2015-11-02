namespace GeocacheApp2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<GeocacheApp2.Models.GeocacheContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GeocacheApp2.Models.GeocacheContext context)
        {
            context.Geocaches.AddOrUpdate(
                g => g.ID,
                new Geocache { Name = "RustonWay", Latitude = 47.278302M, Longitude = -122.471245M },
                new Geocache { Name = "BarCache", Latitude = 47.256219M, Longitude = -122.439684M },
                new Geocache { Name = "TacomaAprk", Latitude = 47.256279M, Longitude = -122.437492M },
                new Geocache { Name = "CoulonPark1", Latitude = 47.504571M, Longitude = -122.205402M },
                new Geocache { Name = "CoulonPark2", Latitude = 47.509912M, Longitude = -122.203063M },
                new Geocache { Name = "Childhood Bakery", Latitude = 47.686891M, Longitude = -122.387325M },
                new Geocache { Name = "The Royal Playfield", Latitude = 47.684601M, Longitude = -122.383715M },
                new Geocache { Name = "Beaches for Days", Latitude = 47.694380M, Longitude = -122.404722M },
                new Geocache { Name = "Golden Park Sunset", Latitude = 47.691997M, Longitude = -122.403735M },
                new Geocache { Name = "The Fishiest of Ladders", Latitude = 47.664576M, Longitude = -122.398392M },
                new Geocache { Name = "BLocks", Latitude = 47.666252M, Longitude = -122.396955M },
                new Geocache { Name = "HotCakes", Latitude = 47.667887M, Longitude = -122.385797M }

            );
            context.SaveChanges();
        }
    }
}
