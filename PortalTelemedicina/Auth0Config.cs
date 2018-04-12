namespace PortalTelemedicina
{
    /// <summary>
    /// POCO class to access appsettings additional configurations.
    /// </summary>
    public class Auth0Config
    {
        public string Domain { get; set; }
        public string ApiIdentifier { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TokenAuthAddress { get; set; }
    }
}
