using MedicinePharmacyBE.DataDomain;
using MedicinePharmacyBE.Repositories.DTOs;

namespace MedicinePharmacyBE.DataDomain.Utilities;

public static class MappingExtensions
{
    public static MedicineResponseDto ToResponseDto(this Medicine medicine) => new()
    {
        Id = medicine.Id,
        FullName = medicine.FullName,
        Notes = medicine.Notes,
        ExpiryDate = medicine.ExpiryDate,
        Quantity = medicine.Quantity,
        Price = medicine.Price,
        Brand = medicine.Brand
    };

    public static Medicine ToEntity(this AddMedicineDto dto) => new()
    {
        FullName = dto.FullName,
        Notes = dto.Notes,
        ExpiryDate = dto.ExpiryDate,
        Quantity = dto.Quantity,
        Price = dto.Price,
        Brand = dto.Brand
    };

    public static void ApplyUpdate(this Medicine medicine, UpdateMedicineDto dto)
    {
        medicine.FullName = dto.FullName;
        medicine.Notes = dto.Notes;
        medicine.ExpiryDate = dto.ExpiryDate;
        medicine.Quantity = dto.Quantity;
        medicine.Price = dto.Price;
        medicine.Brand = dto.Brand;
    }
}
