using Microsoft.AspNetCore.Mvc;
using MedicinePharmacyBE.Repositories.DTOs;
using MedicinePharmacyBE.Services.Interfaces;

namespace MedicinePharmacyBE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly ILogger<SalesController> _logger;

    public SalesController(ISaleService saleService, ILogger<SalesController> logger)
    {
        _saleService = saleService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("GET /api/sales");
        var sales = await _saleService.GetAllAsync();
        return Ok(sales);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddSaleDto dto)
    {
        _logger.LogInformation("POST /api/sales medicineId={Id}, qty={Qty}", dto.MedicineId, dto.QuantitySold);
        try
        {
            var sale = await _saleService.AddAsync(dto);
            return CreatedAtAction(nameof(GetAll), sale);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("POST /api/sales - not found: {Message}", ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("POST /api/sales - bad request: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }
}
