using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GeocacheApp2.Models
{
    public class GeocacheContext : DbContext
    {
        public DbSet<Geocache> Geocaches { get; set; }

        public GeocacheContext() : base("name=GeocacheContext")
        {
            // Add this line for logging purposes
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Geocache>().Property(obj => obj.Latitude).HasPrecision(10, 6);
            modelBuilder.Entity<Geocache>().Property(obj => obj.Longitude).HasPrecision(10, 6);
            base.OnModelCreating(modelBuilder);
        }

        



    }
}
