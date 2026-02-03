using Api_Project.Context;
using Api_Project.Entities;
using Api_Project.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ChefController : ControllerBase
{
    private readonly ApiContext _apiContext;
    public ChefController(ApiContext apiContext)
    {
        _apiContext = apiContext;
    }

    [HttpGet]
    public async Task<IActionResult> ChefGetList()
    {
        var chefs = await _apiContext.Chefs.ToListAsync();
        if (chefs == null || !chefs.Any())
            return NoContent(); 

        var response = new ApiResponse<List<Chef>>
        {
            StatusCode = StatusCodes.Status200OK,
            Message = "Şef Verileri Listelendi",
            Data = chefs
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateChef([FromBody] Chef chef)
    {
        await _apiContext.Chefs.AddAsync(chef);
        await _apiContext.SaveChangesAsync();

        return CreatedAtAction(nameof(ChefGetList), new { id = chef.ChefId },
            new ApiResponse<Chef>
            {
                StatusCode = 201,
                Message = "Şef başarıyla eklendi",
                Data = chef
            });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChef(int id)
    {
        var value = await _apiContext.Chefs.FindAsync(id);
        if (value is null)
            return NotFound(new { Message = "Şef bulunamadı." });

        _apiContext.Chefs.Remove(value);
        await _apiContext.SaveChangesAsync();

        return Ok(new ApiResponse<string>
        {
            StatusCode = StatusCodes.Status200OK,
            Message = "İlgili ID'li Şef silinmiştir."
        });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateChef([FromBody] Chef chef)
    {
        var exists = await _apiContext.Chefs.AnyAsync(x => x.ChefId == chef.ChefId);
        if (!exists) return NotFound("Güncellenecek şef bulunamadı.");

        _apiContext.Chefs.Update(chef);
        await _apiContext.SaveChangesAsync();

        return Ok(new ApiResponse<Chef>
        {
            StatusCode = StatusCodes.Status200OK,
            Message = $"İlgili Şef Güncellendi: {chef.NameSurname}",
            Data = chef
        });
    }
}