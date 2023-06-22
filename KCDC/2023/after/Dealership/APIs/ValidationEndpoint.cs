using FluentValidation;

namespace Dealership.APIs
{
  public class ValidationEndpoint<T> : IEndpointFilter
    where T : class, new()
  {
    private readonly IValidator<T> _validator;

    public ValidationEndpoint(IValidator<T> validator)
    {
      _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext ctx,
      EndpointFilterDelegate next)
    {
      var model = ctx.Arguments[0] as T;
      var result = _validator.Validate(model!);
      if (!result.IsValid)
      {
        return Results.ValidationProblem(result.ToDictionary());
      }
      return await next(ctx);
    }
  }
}