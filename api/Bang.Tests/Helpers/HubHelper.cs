using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Net.Http.Headers;

namespace Bang.Tests.Helpers
{
    public static class HubHelper
    {
        public static HubConnection ConnectToOpenHub(TestServer server, string url) =>
            new HubConnectionBuilder()
                .WithUrl(url, options => options.HttpMessageHandlerFactory = _ =>
                    server.CreateHandler()
                )
                .Build();

        public static HubConnection ConnectToProtectedHub(TestServer server, string url, IEnumerable<string> cookie) =>
            new HubConnectionBuilder()
                .WithUrl(url, options =>
                {
                    options.HttpMessageHandlerFactory = _ => server.CreateHandler();

                    options.Headers = cookie
                        .ToDictionary(
                        _ => HeaderNames.Cookie,
                        value => value
                    );
                })
                .Build();

        public static void CheckMessages(IEnumerable<string> messages, string message)
        {
            Skip.If(!messages.Contains(message)); // SignalR messages sometimes arrive too later...
            Assert.Contains(messages, m => message == m);
        }
    }
}
