using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Venda.DOMAIN.Services
{
    public class ValidacaoService
    {
        public static bool Validar<Email>(Email Obj, out List<ValidationResult> errors)
        {
            errors = new List<ValidationResult>();
            var valido = Validator.TryValidateObject(Obj, new ValidationContext(Obj), errors, true);
            return valido;
        }

        //public class ValidaEmail : ValidationAttribute
        //{
        //    protected override ValidationResult? IsValid(object? email, ValidationContext validationContext)
        //    {
        //        if (email is null || string.IsNullOrWhiteSpace(email.ToString()))
        //        {
        //            return new ValidationResult("O email nao pode ser nulo ou vazio.");
        //        }

        //        string emailStr = email.ToString()!;

        //        if (!emailStr.Contains("@"))
        //        {
        //            return new ValidationResult("O email precisa conter o caractere '@'.");
        //        }

        //        if (!(emailStr.EndsWith(".com") || emailStr.EndsWith(".br")))
        //        {
        //            return new ValidationResult("O email precisa terminar com '.com' ou '.com.br'.");
        //        }

        //        return ValidationResult.Success;

        //    }
        //}
        public class EmailValidationAttribute : ValidationAttribute
        {
            private static readonly Regex EmailRegex = new Regex(
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.Compiled);

            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                if (string.IsNullOrWhiteSpace(value?.ToString()))
                {
                    return new ValidationResult("O e-mail não pode ser nulo ou vazio.");
                }

                string email = value.ToString()!;
                if (!EmailRegex.IsMatch(email))
                {
                    return new ValidationResult("O e-mail fornecido não é válido.");
                }

                return ValidationResult.Success;
            }
        }
    }
}
