using MedicinePharmacyBE.DataDomain;

namespace MedicinePharmacyBE.Repositories.Interfaces;

public interface IMedicineRepository
{
    Task<IEnumerable<Medicine>> GetAllAsync();
    Task<Medicine?> GetByIdAsync(int id);
    Task<Medicine> AddAsync(Medicine medicine);
    Task<Medicine?> UpdateAsync(int id, Medicine medicine);
    Task<bool> DeleteAsync(int id);
}
