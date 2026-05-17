using MedicinePharmacyBE.DataDomain;

namespace MedicinePharmacyBE.Repositories.Interfaces;

public interface ISaleRepository
{
    Task<IEnumerable<MedicineSale>> GetAllAsync();
    Task<MedicineSale> AddAsync(MedicineSale sale);
}
