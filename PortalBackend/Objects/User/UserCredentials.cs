using System.ComponentModel.DataAnnotations;

namespace PortalBackend.Objects.User
{
    public class UserCredentials
    {




        [Required]
        [MaxLength(25)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(75)]
        public string Email { get; set; } = string.Empty;

        public string ProfilePicture { get; set; } = string.Empty;

        [MaxLength(75)]
        [Required]
        public string First_Name { get; set; } = string.Empty;

        [MaxLength(75)]
        [Required]
        public string Last_Name { get; set; } = string.Empty;



    }
}
