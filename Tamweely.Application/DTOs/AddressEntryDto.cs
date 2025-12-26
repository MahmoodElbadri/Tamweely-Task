using System.ComponentModel.DataAnnotations.Schema;
using Tamweely.Domain.Entities;

namespace Tamweely.Application.DTOs;

public class AddressEntryDto
{
    public int Id { get; set; }
    public string Fullname { get; set; }
    public string MobileNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string? PhotoPath { get; set; }
    public string JobTitleName { get; set; }
    public string DepartmentName { get; set; }
    public int Age { get; set; }
}
