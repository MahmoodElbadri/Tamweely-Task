namespace Tamweely.Domain.Entities;

public class JobTitle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<AddressEntry> Addresses { get; set; }
}
