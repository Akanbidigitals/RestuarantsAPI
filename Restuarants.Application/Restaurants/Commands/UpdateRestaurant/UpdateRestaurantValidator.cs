

using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantValidator()
        {
            RuleFor(c => c.Name)
                .Length(3,100);
        }
    }
}
