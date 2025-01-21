

using FluentValidation;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Commands
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(dto => dto.Category)
                 .NotEmpty().WithMessage("Category is required");

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress().WithMessage("Enter Valid Email");

            RuleFor(dto => dto.ContactNumber)
                .Matches(@"^\d{11}$").WithMessage("phone number has to be 11 digits");

            RuleFor(dto => dto.PostalCode)
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Please provide a valid postal code (XX-XXX)");


        }
    }
}
