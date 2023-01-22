using Bang.Tests.Support;

namespace Bang.Tests.Contexts
{
    public class BrowsersContext
    {
        public BrowsersContext()
        {
            this.Cookies = new Dictionary<string, IList<string>>();
            this.PublicHubMessages = new List<string>();
            this.InGameHubMessages = new Dictionary<string, IList<string>>();
        }

        public TestWebApplicationFactory<Program> HttpClientFactory { get; internal set; }
        public IDictionary<string, HttpClient> HttpClients { get; internal set; }
        public IDictionary<string, IList<string>> Cookies { get; internal set; }
        public IList<string> PublicHubMessages { get; internal set; }
        public IDictionary<string, IList<string>> InGameHubMessages { get; internal set; }
    }
}
