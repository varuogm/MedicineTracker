using MedicinePharmacyBE.DataDomain;
using MedicinePharmacyBE.Repositories.DTOs;

namespace MedicinePharmacyBE.Services.Interfaces;

public interface ISaleService
{
    Task<IEnumerable<MedicineSale>> GetAllAsync();
    Task<MedicineSale> AddAsync(AddSaleDto dto);
}
