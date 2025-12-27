using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tamweely.Application.DTOs;
using Tamweely.Application.Interfaces;

namespace Tamweely.Api.Controllers;

[Route("api/address-book")]
[ApiController]
public class AddressBookController(IAddressBookService addressService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAddressBooks()
    {
        var addressBooks = await addressService.GetAllAsync();
        return Ok(addressBooks);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetAddressBookById(int id)
    {
        var addressBook = await addressService.GetByIdAsync(id);
        return Ok(addressBook);
    }

    [HttpGet("search")]
    [Authorize]
    public async Task<IActionResult> SearchAddressBook(string? term, DateTime? from, DateTime? to)
    {
        var addressBooks = await addressService.SearchAsync(term, from, to);
        return Ok(addressBooks);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAddressBook([FromForm] CreateAddressEntryDto addressBookDto)
    {
        var addressBook = await addressService.AddAsync(addressBookDto);
        return CreatedAtAction(nameof(GetAddressBookById), new { id = addressBook.Id }, addressBook);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateAddressBook(int id, [FromForm] CreateAddressEntryDto addressBookDto)
    {
        var entity = await addressService.UpdateAsync(id, addressBookDto);
        return Ok(entity);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteAddressBook(int id)
    {
        await addressService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("asExcel")]
    [Authorize]
    public async Task<IActionResult> ExportToExcel(string? term, DateTime? from, DateTime? to)
    {
        var file = await addressService.ExportAsync(term, from, to);
        return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AddressBook.xlsx");
    }

}
