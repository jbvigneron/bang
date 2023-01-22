using Bang.Tests.Support;

namespace Bang.Tests.Contexts
{
    public class BrowsersContext
    {
        public BrowsersContext()
        {
            this.Cookies = new Dictionary<string, IList<string>>();
            this.PublicHubMessages = new List<string>();
            this.GameHubMessages = new Dictionary<string, IList<string>>();
            this.PlayerHubMessages = new Dictionary<string, IList<string>>();
        }

        public TestWebApplicationFactory<Program>? HttpClientFactory { get; set; }
        public IDictionary<string, HttpClient>? HttpClients { get; set; }
        public IDictionary<string, IList<string>> Cookies { get; }
        public IList<string> PublicHubMessages { get; }
        public IDictionary<string, IList<string>> GameHubMessages { get; }
        public IDictionary<string, IList<string>> PlayerHubMessages { get; }
    }
}
