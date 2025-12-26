using Tamweely.Application.DTOs;

namespace Tamweely.Application.Interfaces;

public interface IAddressBookService
{
    //GetAll, GetById, Add, Delete
    Task<List<AddressEntryDto>> GetAllAsync();
    Task<AddressEntryDto> GetByIdAsync(int id);
    Task<AddressEntryDto> AddAsync(CreateAddressEntryDto addressBook);
    Task DeleteAsync(int id);
    Task<List<AddressEntryDto>> SearchAsync(string search, DateTime? fromDate, DateTime? toDate);
}
