using Microsoft.AspNetCore.Http;

namespace Tamweely.Application.DTOs;

public class CreateAddressEntryDto
{
    public string Fullname { get; set; }
    public string MobileNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public IFormFile? Photo { get; set; }
    public int JobTitleId { get; set; }
    public int DepartmentId { get; set; }
}
