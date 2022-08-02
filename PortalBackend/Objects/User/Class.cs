namespace PortalBackend.Objects.User
{

    class TokenKEYVAL
    {
        public string username { get; set; }
        public string RefreshToken { get; set; }

        public DateTime ExpiresIn { get; set; }
    }
}
