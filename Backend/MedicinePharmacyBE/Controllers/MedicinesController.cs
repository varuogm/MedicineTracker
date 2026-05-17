using Microsoft.AspNetCore.Mvc;
using MedicinePharmacyBE.Repositories.DTOs;
using MedicinePharmacyBE.Services.Interfaces;

namespace MedicinePharmacyBE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicinesController : ControllerBase
{
    private readonly IMedicineService _medicineService;
    private readonly ILogger<MedicinesController> _logger;

    public MedicinesController(IMedicineService medicineService, ILogger<MedicinesController> logger)
    {
        _medicineService = medicineService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search = null)
    {
        _logger.LogInformation("Get api called search='{Search}'", search);
        var medicines = await _medicineService.GetAllAsync(search);
        return Ok(medicines);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("Get api called id={Id}", id);
        var medicine = await _medicineService.GetByIdAsync(id);
        if (medicine == null)
        {
            _logger.LogWarning("Get api called id={Id} - not found", id);
            return NotFound();
        }
        return Ok(medicine);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddMedicineDto dto)
    {
        _logger.LogInformation("POST api called with '{Name}'", dto.FullName);
        var created = await _medicineService.AddAsync(dto);
        return Ok(created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMedicineDto dto)
    {
        _logger.LogInformation("PUT api called for id={Id}", id);
        var updated = await _medicineService.UpdateAsync(id, dto);
        if (updated == null)
        {
            _logger.LogWarning("PUT api called for id={Id} - not found", id);
            return NotFound();
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("DELETE api called for id={Id}", id);
        var isdeleted = await _medicineService.DeleteAsync(id);
        if (!isdeleted)
        {
            _logger.LogWarning("DELETE api called for id={Id} - not found", id);
            return NotFound();
        }
        return NoContent();
    }
}
