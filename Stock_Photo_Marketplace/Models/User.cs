using System.ComponentModel.DataAnnotations;

namespace Stock_Photo_Marketplace.Models
{
    public class User
    {
        public int UserID { get; set; } // Primary Key

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; } // Store as plain text

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
    }
}
