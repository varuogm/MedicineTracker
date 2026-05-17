using Microsoft.EntityFrameworkCore;
using MedicinePharmacyBE.DataDomain;
using MedicinePharmacyBE.Repositories.Interfaces;
using MedicinePharmacyBE.Repositories.MedicineContext;

namespace MedicinePharmacyBE.Repositories;

public class MedicineRepository : IMedicineRepository
{
    private readonly DataContext _context;
    private readonly ILogger<MedicineRepository> _logger;

    public MedicineRepository(DataContext context, ILogger<MedicineRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Medicine>> GetAllAsync()
    {
        _logger.LogInformation("Repository: fetching all medicines");
        return await _context.Medicines.ToListAsync();
    }

    public async Task<Medicine?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Repository: fetching medicine with id {Id}", id);
        return await _context.Medicines.FindAsync(id);
    }

    public async Task<Medicine> AddAsync(Medicine medicine)
    {
        _logger.LogInformation("Repository: adding medicine '{Name}'", medicine.FullName);
        _context.Medicines.Add(medicine);
        await _context.SaveChangesAsync();
        return medicine;
    }

    public async Task<Medicine?> UpdateAsync(int id, Medicine medicine)
    {
        var existing = await _context.Medicines.FindAsync(id);
        if (existing == null)
        {
            _logger.LogWarning("Repository: medicine with id {Id} not found for update", id);
            return null;
        }

        existing.FullName = medicine.FullName;
        existing.Notes = medicine.Notes;
        existing.ExpiryDate = medicine.ExpiryDate;
        existing.Quantity = medicine.Quantity;
        existing.Price = medicine.Price;
        existing.Brand = medicine.Brand;

        await _context.SaveChangesAsync();
        _logger.LogInformation("Repository: updated medicine with id {Id}", id);
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var medicine = await _context.Medicines.FindAsync(id);
        if (medicine == null)
        {
            _logger.LogWarning("Repository: medicine with id {Id} not found for delete", id);
            return false;
        }

        _context.Medicines.Remove(medicine);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Repository: deleted medicine with id {Id}", id);
        return true;
    }
}
