using Barber.Colocho.Application.DTO.User;
using FluentValidation;

namespace Barber.Colocho.Transversal.Validations.User
{
    public class UserValidator : AbstractValidator<UserApplicationDto>
    {
        public UserValidator() 
        {
            RuleFor(c=>c.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("Los apellidos no deben estar vacios");
            RuleFor(x => x.Birthdate).NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.").Must(BeAtLeast18YearsOld).WithMessage("Debes ser mayor de 18 años.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("El correo no debe estar vacio").EmailAddress().WithMessage("El correo es invalido");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("El número de teléfono es obligatorio.").Matches(@"^\d{10}$").WithMessage("El número debe tener exactamente 10 dígitos (sin +52).");
            RuleFor(x => x.Pass).NotEmpty().WithMessage("La contraseña es obligatoria.").MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.").Matches(@"[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.").Matches(@"[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula.").Matches(@"\d").WithMessage("La contraseña debe contener al menos un número.").Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("La contraseña debe contener al menos un carácter especial.");
        }

        private bool BeAtLeast18YearsOld(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
