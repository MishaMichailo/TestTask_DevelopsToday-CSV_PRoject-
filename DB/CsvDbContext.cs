using Microsoft.EntityFrameworkCore;


namespace TestTask_DevelopsToday.DB
{
    public class CsvDbContext : DbContext
    {
        public DbSet<DataTrip> DataTrips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=WIN-HE5A1SMBDQ9;Database=TaxiDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
