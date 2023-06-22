using Dealership.Data;
using FluentValidation;

namespace Dealership.Validators;

public class CarValidator : AbstractValidator<Vehicle>
{
  public CarValidator()
  {
    RuleFor(c => c.Make)
      .NotEmpty()
      .MinimumLength(5);



  }
}
