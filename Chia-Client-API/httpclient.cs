using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace Chia_Client_API
{
    public class httpclient
    {
        public static void Test()
        {
            var handler = new HttpClientHandler();
            X509Certificate2 privateCertificate = CertificateLoader.GetCertificate(Endpoint.wallet);
                handler.ServerCertificateCustomValidationCallback = (requestMessage, certificate, chain, policyErrors) => true;
                handler.ClientCertificates.Add(privateCertificate);
            using (var httpClient = new HttpClient(handler))
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://localhost:9256/get_sync_status"))
                {
                    request.Content = new StringContent(" { } ");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = httpClient.Send(request);
                    if (response.IsSuccessStatusCode)
                    {
                        { }
                    }
                }
            }
            { }
        }
    }
}
