using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    ///DTO representing a user.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name of the user.
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// Last name of the user.
        /// </summary>
        public string LastName { get; set; }


        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Represents if user is admin (admin - true; not admin - false).
        /// </summary>
        public bool IsAdmin { get; set; }


        /// <summary>
        /// Password of the user.
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        ///  Date of birth for user.
        /// </summary>
        public DateTime DateOfBirth { get; set; }


        /// <summary>
        /// Country of the user.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// City of the user.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Address of the user.
        /// </summary>
        public string Address { get; set; }
    }
}
