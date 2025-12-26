using AutoMapper;
using Tamweely.Application.DTOs;
using Tamweely.Application.Interfaces;
using Tamweely.Domain.Entities;
using Tamweely.Domain.Exceptions;

namespace Tamweely.Infrastructure.Services;

public class AddressBookService(IGenericRepository<AddressEntry> repo, IMapper mapper) : IAddressBookService
{
    public async Task<AddressEntryDto> AddAsync(CreateAddressEntryDto addressBook)
    {
        var address = mapper.Map<AddressEntry>(addressBook);
        if (addressBook.Photo != null && addressBook.Photo.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder); // يتأكد إن الفولدر موجود
            }
            var fileName = addressBook.Photo.FileName.Trim();
            fileName = $"{Guid.NewGuid()}{addressBook.Fullname}{Path.GetExtension(addressBook.Photo.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await addressBook.Photo.CopyToAsync(stream);
            }

            var relativePath = $"/images/{fileName}";
            address.PhotoPath = relativePath;
        }
        await repo.AddAsync(address);
        var addressDto = mapper.Map<AddressEntryDto>(address);
        return addressDto;
    }

    public async Task DeleteAsync(int id)
    {
        await repo.DeleteAsync(id);
    }

    public async Task<List<AddressEntryDto>> GetAllAsync()
    {
        var entities = await repo.FindAsync(tmp => true, new string[] { "Department", "JobTitle" });
        var addressDtos = mapper.Map<List<AddressEntryDto>>(entities);
        return addressDtos;
    }

    public async Task<AddressEntryDto> GetByIdAsync(int id)
    {
        var entities = await repo.FindAsync(x => x.Id == id, new string[] { "Department", "JobTitle" });
        var entity = entities.FirstOrDefault();
        if (entity == null)
        {
            throw new NotFoundException(nameof(AddressEntry), id.ToString());
        }
        var addressDto = mapper.Map<AddressEntryDto>(entity);
        return addressDto;
    }

    public async Task<AddressEntryDto> UpdateAsync(int id, CreateAddressEntryDto addressBook)
    {
        var existedEntity = await repo.GetByIdAsync(id);
        if (existedEntity == null)
        {
            throw new NotFoundException(nameof(AddressEntry), id.ToString());
        }
        var oldPathString = existedEntity.PhotoPath;

        mapper.Map(addressBook, existedEntity);
        if (addressBook.Photo != null && addressBook.Photo.Length > 0)
        {
            if (!string.IsNullOrEmpty(oldPathString))
            {
                var oldFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldPathString.TrimStart('/'));
                if (System.IO.File.Exists(oldFullPath))
                {
                    System.IO.File.Delete(oldFullPath);
                }
            }
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(addressBook.Photo.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await addressBook.Photo.CopyToAsync(stream);
            }
            existedEntity.PhotoPath = $"/images/{fileName}";
        }
        else
        {
            existedEntity.PhotoPath = oldPathString;
        }
        await repo.UpdateAsync(existedEntity);
        return mapper.Map<AddressEntryDto>(existedEntity);
    }

    public async Task<List<AddressEntryDto>> SearchAsync(string search, DateTime? fromDate, DateTime? toDate)
    {
        string[] includes = { "JobTitle", "Department" };

        var entities = await repo.FindAsync(tmp =>
            (string.IsNullOrEmpty(search) || tmp.Fullname.Contains(search) || tmp.MobileNumber.Contains(search) || tmp.Address.Contains(search) || tmp.Email.Contains(search)) &&
            (!fromDate.HasValue || tmp.DateOfBirth >= fromDate) &&
            (!toDate.HasValue || tmp.DateOfBirth <= toDate),
            includes
        );

        return mapper.Map<List<AddressEntryDto>>(entities);
    }
}

