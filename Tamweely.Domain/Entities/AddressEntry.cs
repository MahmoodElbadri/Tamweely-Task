using System.ComponentModel.DataAnnotations.Schema;

namespace Tamweely.Domain.Entities;

public class AddressEntry
{
    public int Id { get; set; }
    public string Fullname { get; set; }
    public string MobileNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string? PhotoPath { get; set; }
    public int JobTitleId { get; set; }
    public JobTitle JobTitle { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    [NotMapped]
    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
