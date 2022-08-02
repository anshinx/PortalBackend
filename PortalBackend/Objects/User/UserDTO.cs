using System.ComponentModel.DataAnnotations;

namespace PortalBackend.Objects.User
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        [MaxLength(75)]
        public string Email { get; set; } = string.Empty;

        public string ProfilePicture { get; set; } = string.Empty;

        public int StudentId { get; set; }

        public string Department { get; set; } = string.Empty;

        [MaxLength(75)]
        [Required]
        public string First_Name { get; set; } = string.Empty;

        [MaxLength(75)]
        [Required]
        public string Last_Name { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public List<Communities> Communities { get; set; } = new List<Communities>();

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }




    }
}
