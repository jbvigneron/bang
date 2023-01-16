using Bang.Tests.Support;

namespace Bang.Tests.Contexts
{
    public class BrowserContext
    {
        public TestWebApplicationFactory<Program> HttpClientFactory { get; internal set; }
        public IDictionary<string, HttpClient> HttpClients { get; internal set; }
        public IDictionary<string, IList<string>> Cookies { get; internal set; }
        public IDictionary<string, IList<string>> SignalRMessages { get; internal set; }
    }
}
