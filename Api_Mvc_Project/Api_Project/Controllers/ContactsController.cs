using Api_Project.Context;
using Api_Project.DTOs.ContactDtos;
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
    public class ContactsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ContactsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet] 
         public async Task<IActionResult> GetContactsList()
        {
            var contactList = await _context.Contacts.ToListAsync();
            if (!contactList.Any())
                return StatusCode(StatusCodes.Status204NoContent);
            var mapValue = _mapper.Map<List<ResultContactDto>>(contactList);
            return Ok(mapValue);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContact)
        {
            var createCont = _mapper.Map<Contact>(createContact);
            await _context.Contacts.AddAsync(createCont);
            await _context.SaveChangesAsync();
            return Ok("Ekleme işlemi başarılı");
        }
        [HttpDelete]

        public async Task<IActionResult> DeleteContact(int id)
        {
            var value = await _context.Contacts.FindAsync(id);
             _context.Contacts.Remove(value);
            return Ok("Silme işlemi başarıyla gerçekleşti");
        }


        [HttpGet("GetByIdContact")]
        public async Task<IActionResult> GetByIdContact(int id)
        {
            var value = await _context.Contacts.FindAsync(id);
            if (value == null) return NoContent();
            return Ok(_mapper.Map<GetByIdContactDto>(value));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            var value = _mapper.Map<Contact>(updateContactDto);
            if (value == null) return NoContent();
            _context.Contacts.Update(value);
            return Ok("Günceleme işlemi gerçekleşti");
        }
    }
}
