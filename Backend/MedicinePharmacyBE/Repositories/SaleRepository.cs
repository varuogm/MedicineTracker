using Microsoft.EntityFrameworkCore;
using MedicinePharmacyBE.DataDomain;
using MedicinePharmacyBE.Repositories.Interfaces;
using MedicinePharmacyBE.Repositories.MedicineContext;

namespace MedicinePharmacyBE.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DataContext _context;
    private readonly ILogger<SaleRepository> _logger;

    public SaleRepository(DataContext context, ILogger<SaleRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MedicineSale>> GetAllAsync()
    {
        _logger.LogInformation("Repository: fetching all sales");
        return await _context.MedicineSales.ToListAsync();
    }

    public async Task<MedicineSale> AddAsync(MedicineSale sale)
    {
        _logger.LogInformation("Repository: recording sale for medicine id {MedicineId}, qty {Qty}",
            sale.MedicineId, sale.QuantitySold);
        _context.MedicineSales.Add(sale);
        await _context.SaveChangesAsync();
        return sale;
    }
}
