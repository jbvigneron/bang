namespace Bang.Tests.Contexts
{
    public class BrowsersContext
    {
        public BrowsersContext()
        {
            this.Cookies = new Dictionary<string, IEnumerable<string>>();
        }

        public IDictionary<string, HttpClient>? HttpClients { get; set; }
        public IDictionary<string, IEnumerable<string>> Cookies { get; }
    }
}
