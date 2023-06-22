using Dealership.Data;
using FluentValidation;

namespace Dealership.Validators
{
  public class EmployeeValidator : AbstractValidator<Employee>
  {
    public EmployeeValidator()
    {
      RuleFor(c => c.FirstName)
        .NotEmpty()
        .MinimumLength(5);
    }
  }
}
