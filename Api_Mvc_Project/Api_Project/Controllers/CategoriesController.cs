using Api_Project.Context;
using Api_Project.Entities;
using Api_Project.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Api_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiContext _context;
       public CategoriesController(ApiContext apiContext) {
        _context = apiContext;   
        }
        [HttpGet]
        public async Task<IActionResult> CategoriesGetList() {
          var categories = await _context.Categories.ToListAsync();
            if (!categories.Any())
                return StatusCode(StatusCodes.Status204NoContent);
            var response = new ApiResponse<List<Category>>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Kategori Verileri Listelendi",
                Data = categories   
            };
            return StatusCode(StatusCodes.Status200OK,response);
        }

        [HttpPost]
        public async Task<IActionResult>CreateCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok("Kategoriler Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id) {
            var value = await _context.Categories.FindAsync(id);
            if(value is null) return StatusCode(StatusCodes.Status404NotFound);
            _context.Categories.Remove(value);
            var response = new ApiResponse<string> { StatusCode = StatusCodes.Status200OK,
                Message = "İlgili ID'li Kategori Silinmiştir"
             };
            await _context.SaveChangesAsync();
            return Ok(new ApiResponse<string>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "İlgili ID'li kategori silinmiştir."
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategories(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return Ok(new ApiResponse<string>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = $"İlgili Kategori Güncellendi:{category.Name}",
            });


        }
    }
}
