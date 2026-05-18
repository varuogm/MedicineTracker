using MedicinePharmacyBE.DataDomain.Utilities;
using MedicinePharmacyBE.Repositories.DTOs;
using MedicinePharmacyBE.Repositories.Interfaces;
using MedicinePharmacyBE.Services.Interfaces;

namespace MedicinePharmacyBE.Services;

public class MedicineService : IMedicineService
{
    private readonly IMedicineRepository _medicineRepository;
    private readonly ILogger<MedicineService> _logger;

    public MedicineService(IMedicineRepository medicineRepository, ILogger<MedicineService> logger)
    {
        _medicineRepository = medicineRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<MedicineResponseDto>> GetAllAsync(string? search = null)
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
        _logger.LogInformation("Service: getting all medicines, search='{Search}'", search);

        var medicines = await _medicineRepository.GetAllAsync();
        if (!string.IsNullOrWhiteSpace(search))
        {
            medicines = medicines.Where(m => m.FullName.Contains(search, StringComparison.OrdinalIgnoreCase) ||m.Brand.Contains(search, StringComparison.OrdinalIgnoreCase));

            _logger.LogInformation("Service: filtered to {Count} medicines matching '{Search}'",medicines.Count(), search);
        }

        return medicines.Select(m => m.ToResponseDto());
    }

    public async Task<MedicineResponseDto?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Service: getting medicine id {Id}", id);

        var medicine = await _medicineRepository.GetByIdAsync(id);
        if (medicine == null)
        {
            _logger.LogWarning("Service: medicine id {Id} not found", id);
            return null;
        }

        return medicine.ToResponseDto();
    }

    public async Task<MedicineResponseDto> AddAsync(AddMedicineDto dto)
    {
        _logger.LogInformation("Service: adding medicine '{Name}'", dto.FullName);
        var created = await _medicineRepository.AddAsync(dto.ToEntity());
        _logger.LogInformation("Service: medicine created with id {Id}", created.Id);

        return created.ToResponseDto();
    }

    public async Task<MedicineResponseDto?> UpdateAsync(int id, UpdateMedicineDto dto)
    {
        _logger.LogInformation("Service: updating medicine id {Id}", id);
        var existingData = await _medicineRepository.GetByIdAsync(id);

        if (existingData == null)
        {
            _logger.LogWarning("Service: medicine id {Id} not found for upodation", id);
            return null;
        }

        existingData.ApplyUpdate(dto);
        var updated = await _medicineRepository.UpdateAsync(id, existingData);
        return updated?.ToResponseDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation("Service: deleting medicine id {Id}", id);
        var isdeleted = await _medicineRepository.DeleteAsync(id);

        if (!isdeleted)
            _logger.LogWarning("Service: medicine id {Id} not found for delete", id);

        return isdeleted;
    }
}
