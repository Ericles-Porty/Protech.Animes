using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Protech.Animes.Application.Validations;

public class HumanNamePattern : ValidationAttribute
{

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        Console.WriteLine("Validating name");
        if (value is null)
        {
            return new ValidationResult("Name is required.");
        }

        if (value is string name)
        {
            name = name.Trim();

            if (name.Length < 3)
            {
                return new ValidationResult("Name must have at least 3 characters.");
            }

            if (name.Length > 100)
            {
                return new ValidationResult("Name must have at most 100 characters.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return new ValidationResult("Name can not be empty.");
            }

            if (!Regex.IsMatch(name, @"^[a-zA-ZÀ-ú\s-]+$"))
            {
                return new ValidationResult("This isn't a valid name.");
            }

            return ValidationResult.Success!;
        }

        return new ValidationResult("Invalid name.");
    }
}