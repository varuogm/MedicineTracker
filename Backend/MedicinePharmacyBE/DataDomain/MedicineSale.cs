namespace MedicinePharmacyBE.DataDomain;

public class MedicineSale
{
    public int Id { get; set; }
    public int MedicineId { get; set; }
    public int QuantitySold { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime SaleDate { get; set; }
}
