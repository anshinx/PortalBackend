namespace PortalBackend.Objects.User
{
    public class RefreshToken
    {
        public string Token { get; set; }

        public DateTime ExpiresIn { get; set; }

        public DateTime GeneratedIn { get; set; }


    }
}
