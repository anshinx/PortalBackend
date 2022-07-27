using System.ComponentModel.DataAnnotations;

namespace PortalBackend.Objects.User
{
    public class Subscriptions
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public int SubscriptedUserId { get; set; }

        [Required]
        [MaxLength(100)]
        public int SubscriptionId { get; set; }

        [Required]
        [MaxLength(20)]
        public string SubscriptionName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string SubscriptionType { get; set; } = string.Empty;

        public DateTime WhenToSubbed { get; set; }

    }
}
