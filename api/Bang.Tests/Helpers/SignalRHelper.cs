using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Net.Http.Headers;

namespace Bang.Tests.Helpers
{
    public static class SignalRHelper
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
    }
}
