namespace API.Core
{
    public class ApplicationSettings
    {
        public string ConnectionString { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public string BugsnagKey { get; set; }
    }
}
