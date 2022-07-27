using System.ComponentModel.DataAnnotations;

namespace PortalBackend.Objects.User
{
    public class Communities
    {
        [Required]
        [MaxLength(100)]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(100)]
        public bool IsActive { get; set; }

        public DateTime WhenAttend { get; set; }

        public DateTime WhenLeft { get; set; }

        [Required]
        [MaxLength(100)]
        public int RoleID { get; set; }
    }
}
