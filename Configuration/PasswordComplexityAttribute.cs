using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SportEdge.API.Configuration
{
    /// <summary>
    /// Custom validation attribute to check if a password meets complexity requirements.
    /// </summary>
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        public int MinimumLength { get; set; } = 8;
        public bool RequireUppercase { get; set; } = true;
        public bool RequireLowercase { get; set; } = true;
        public bool RequireDigit { get; set; } = true;

        public PasswordComplexityAttribute()
        {
            ErrorMessage = "Password does not meet the complexity requirements.";
        }

        public override bool IsValid(object? value)
        {
            if (value is string password)
            {
                if (password.Length < MinimumLength)
                {
                    ErrorMessage = $"Password must be at least {MinimumLength} characters long.";
                    return false;
                }

                if (RequireUppercase && !Regex.IsMatch(password, @"[A-Z]"))
                {
                    ErrorMessage = "Password must contain at least one uppercase letter.";
                    return false;
                }

                if (RequireLowercase && !Regex.IsMatch(password, @"[a-z]"))
                {
                    ErrorMessage = "Password must contain at least one lowercase letter.";
                    return false;
                }

                if (RequireDigit && !Regex.IsMatch(password, @"\d"))
                {
                    ErrorMessage = "Password must contain at least one digit.";
                    return false;
                }

                return true;
            }

            return false;
        }


    }
}
