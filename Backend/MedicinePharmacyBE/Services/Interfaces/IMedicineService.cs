using MedicinePharmacyBE.Repositories.DTOs;

namespace MedicinePharmacyBE.Services.Interfaces;

public interface IMedicineService
{
    Task<IEnumerable<MedicineResponseDto>> GetAllAsync(string? search = null);
    Task<MedicineResponseDto?> GetByIdAsync(int id);
    Task<MedicineResponseDto> AddAsync(AddMedicineDto dto);
    Task<MedicineResponseDto?> UpdateAsync(int id, UpdateMedicineDto dto);
    Task<bool> DeleteAsync(int id);
}
