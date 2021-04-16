using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nimb3s.Identity.Samples.Client
{
    class Program
    {
        const string BaseUrl = "https://localhost:18089";

        static async Task Main()
        {
            Console.WriteLine("Hello World!");

            await RequestTokenUsingClientCredentialsAsync();
        }

        private static async Task RequestTokenUsingClientCredentialsAsync()
        {
            var client = new HttpClient(new LogHandler());

            var response = await client.RequestTokenAsync(new TokenRequest
            {
                Address = $"{BaseUrl}/connect/token",
                GrantType = "client_credentials",

                ClientId = "Nimb3sSPA",
                ClientSecret = "1234",
                Parameters =
                {
                    { "custom_parameter", "custom value"},
                    { "scope", "api1" }
                }
            });
        }
    }
}
