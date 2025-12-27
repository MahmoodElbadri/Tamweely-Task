using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Tamweely.Application.DTOs;

namespace Tamweely.Application.Validations.AddressEntry;

public class CreateAddressEntryValidations : AbstractValidator<CreateAddressEntryDto>
{
    public CreateAddressEntryValidations()
    {
        RuleFor(tmp => tmp.Address)
             .NotEmpty();

        RuleFor(tmp => tmp.Email)
            .NotEmpty()
            .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")
            .WithMessage("Invalid email address");

        RuleFor(tmp=>tmp.MobileNumber)
            .NotEmpty()
            .Matches(@"^01[0-2,5]{1}[0-9]{8}$")
            .WithMessage("Invalid Egyptian mobile number");

        RuleFor(tmp=>tmp.DateOfBirth)
            .NotEmpty();

    }
}
