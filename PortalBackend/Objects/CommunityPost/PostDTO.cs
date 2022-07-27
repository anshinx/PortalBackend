namespace PortalBackend.Objects.CommunityPost
{
    public class PostDTO
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int CommunityID { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<string> Likes { get; set; }

        public string PhotoURL { get; set; }

        public string UserID { get; set; }


    }
}
