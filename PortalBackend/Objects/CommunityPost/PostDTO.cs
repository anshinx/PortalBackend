using System.ComponentModel.DataAnnotations;

namespace PortalBackend.Objects.CommunityPost
{
    public class PostDTO
    {
        public int ID { get; set; }

        [MaxLength(144)]
        public string Title { get; set; }

        public int CommunityID { get; set; }

        public DateTime CreatedDate { get; set; }



        public string PhotoURL { get; set; }

        public string UserID { get; set; }


    }
}
