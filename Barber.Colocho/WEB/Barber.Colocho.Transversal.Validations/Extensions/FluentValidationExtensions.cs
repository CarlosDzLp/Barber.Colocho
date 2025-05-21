using FluentValidation;
using FluentValidation.Results;

namespace Barber.Colocho.Transversal.Validations.Extensions
{
    public static class FluentValidationExtensions
    {
        public static async Task<string> ValidateAsStringAsync<T>(this IValidator<T> validator, T instance)
        {
            ValidationResult result = await validator.ValidateAsync(instance);

            if (!result.IsValid)
            {
                var errors = result.Errors
            .Select((e, i) => $"{i + 1}. {e.ErrorMessage}");

                return string.Join(Environment.NewLine, errors);
            }
            return string.Empty;
        }
    }
}
