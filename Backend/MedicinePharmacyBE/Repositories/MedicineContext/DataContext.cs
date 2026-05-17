using Microsoft.EntityFrameworkCore;
using MedicinePharmacyBE.DataDomain;

namespace MedicinePharmacyBE.Repositories.MedicineContext;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Medicine> Medicines => Set<Medicine>();
    public DbSet<MedicineSale> MedicineSales => Set<MedicineSale>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.FullName).IsRequired().HasMaxLength(200);
            entity.Property(m => m.Brand).IsRequired().HasMaxLength(100);
            entity.Property(m => m.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<MedicineSale>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.TotalPrice).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Medicine>().HasData(
            new Medicine
            {
                Id = 1,
                FullName = "Paracetamol",
                Notes = "Fever medicine",
                ExpiryDate = new DateTime(2026, 5, 1),
                Quantity = 50,
                Price = 20.50m,
                Brand = "Cipla"
            }
        );
    }
}
