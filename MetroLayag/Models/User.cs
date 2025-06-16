using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetroLayag.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Station { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } // "MainAdmin" or "StationAdmin"

        [NotMapped]
        [Required]
        public string Password { get; set; }
    }
}
