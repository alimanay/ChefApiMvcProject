using Api_Project.Context;
using Api_Project.DTOs.FeatureDtos;
using Api_Project.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _apiContext;
        public FeaturesController(IMapper mapper,ApiContext context)
        {
            _mapper = mapper;
            _apiContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeatureList()
        {
            var featureList = await _apiContext.Features.ToListAsync();
            return Ok(_mapper.Map<List<ResultFeatureDto>>(featureList));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
          var createFeature = _mapper.Map<Feature>(createFeatureDto);
           await _apiContext.Features.AddAsync(createFeature);
            await _apiContext.SaveChangesAsync();
            return Ok("Ekleme işlemi Başarılı");
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteFeature(int id)
        {
            var value = await _apiContext.Features.FindAsync(id);
             _apiContext.Features.Remove(value);
            return Ok("Silme işlemi başarıyla gerçekleşti");
        }


        [HttpGet("GetByIdFeature")]
        public async Task<IActionResult> GetByIdFeature(int id)
        {
            var value = await _apiContext.Features.FindAsync(id);
            return Ok(_mapper.Map<GetByIdFeatureDto>(value));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var value = _mapper.Map<Feature>(updateFeatureDto);
            _apiContext.Features.Update(value);
            return Ok("Günceleme işlemi gerçekleşti");
        }
    }
}
