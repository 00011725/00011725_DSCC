namespace _00011725.AppData
{
    public class AppData
    {
        private readonly IConfiguration _configuration;

        public AppData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetUrl()
        {
            return _configuration["BaseUrl"];
        }
    }
}
