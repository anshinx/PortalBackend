namespace PortalBackend.Objects.User
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresIn { get; set; }  = DateTime.Now.AddDays(2);

        public DateTime GeneratedIn { get; set; } = DateTime.Now;


    }
}
