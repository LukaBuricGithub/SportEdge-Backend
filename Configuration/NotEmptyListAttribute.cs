using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Configuration
{
    /// <summary>
    /// Custom validation attribute to check if a list of integers is not empty.
    /// </summary>
    public class NotEmptyListAttribute : ValidationAttribute
    {

        /// <summary>
        /// Checks whether the specified value is a non-empty list of integers.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>True if the value is a non-empty list of integers;
        public override bool IsValid(object? value)
        {
            if (value is IList<int> list)
            {
                return list.Any();
            }
            return false;
        }
    }
}
