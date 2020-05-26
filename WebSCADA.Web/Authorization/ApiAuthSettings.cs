namespace WebSCADA.Web.Authorization
{
    public class ApiAuthSettings
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public int ExpirationTimeInSeconds { get; set; }
    }
}
