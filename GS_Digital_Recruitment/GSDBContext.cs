using GS_Digital_Recruitment.Models;
using System;
using System.Web;
using System.Data.Entity;

namespace GS_Digital_Recruitment
{
    public partial class GSDbContext : DbContext
    {
        public DbSet<MasterUser> MasterUser { get; set; }
        public DbSet<MasterUserRecruiter> MasterUserRecruiter { get; set; }
        public DbSet<MasterDivision> MasterDivision { get; set; }
        public DbSet<ManageForecast> ManageForecast { get; set; }
        public DbSet<MasterCustomer> MasterCustomer { get; set; }
        public DbSet<TransaksiSuratJaminan> TransaksiSuratJaminan { get; set; }


        public GSDbContext() : base("name=GSDbContext") { }

        public GSDbContext(string dbSource, string dbName, string dbUsers, string dbPass)
            : base($"Data Source=" + dbSource + ";initial catalog=" + dbName + ";User Id=" + dbUsers + ";Password=" + dbPass + "; ") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<GSDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

    }

    public partial class GSDbContextGSTrack : DbContext
    {

        public GSDbContextGSTrack() : base("name=GSDbContextGSTrack") { }

        public GSDbContextGSTrack(string dbSource, string dbName, string dbUsers, string dbPass)
            : base($"Data Source=" + dbSource + ";initial catalog=" + dbName + ";User Id=" + dbUsers + ";Password=" + dbPass + "; ") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<GSDbContextGSTrack>(null);
            base.OnModelCreating(modelBuilder);
        }

    }
}
