using MedicinePharmacyBE.DataDomain;
using MedicinePharmacyBE.Repositories.DTOs;
using MedicinePharmacyBE.Repositories.Interfaces;
using MedicinePharmacyBE.Services.Interfaces;

namespace MedicinePharmacyBE.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMedicineRepository _medicineRepository;
    private readonly ILogger<SaleService> _logger;

    public SaleService(
        ISaleRepository saleRepository,
        IMedicineRepository medicineRepository,
        ILogger<SaleService> logger)
    {
        _saleRepository = saleRepository;
        _medicineRepository = medicineRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<MedicineSale>> GetAllAsync()
    {
        try
        {
            _logger.LogInformation("Service: getting all sales");
            return await _saleRepository.GetAllAsync();

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<MedicineSale> AddAsync(AddSaleDto saleDto)
    {
        try
        {
            _logger.LogInformation("Service: processing sale for medicine id {Id}, qty {Qty}", saleDto.MedicineId, saleDto.QuantitySold);

            var medicine = await _medicineRepository.GetByIdAsync(saleDto.MedicineId)
                ?? throw new KeyNotFoundException($"Medicine with id {saleDto.MedicineId} not found.");

            if (medicine.Quantity < saleDto.QuantitySold)
            {
                _logger.LogWarning("Service: insufficient stock for medicine id {Id}. Available: {Available}, Requested: {Requested}", saleDto.MedicineId, medicine.Quantity, saleDto.QuantitySold);

                throw new InvalidOperationException($"Insufficient stock. Available: {medicine.Quantity}, Requested: {saleDto.QuantitySold}.");
            }

            medicine.Quantity -= saleDto.QuantitySold;
            await _medicineRepository.UpdateAsync(medicine.Id, medicine);

            var sale = new MedicineSale
            {
                MedicineId = saleDto.MedicineId,
                QuantitySold = saleDto.QuantitySold,
                TotalPrice = medicine.Price * saleDto.QuantitySold,
                SaleDate = DateTime.UtcNow
            };

            var added = await _saleRepository.AddAsync(sale);

            _logger.LogInformation("Service: sale recorded with id {SaleId}, total {Total}", added.Id, added.TotalPrice);

            return added;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
