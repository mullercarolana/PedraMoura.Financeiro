using ApplicationCore.Seedwork.Exceptions;
using ApplicationCore.Shared.Certificates;
using ApplicationCore.Shared.Urls;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace ApplicationCore.Services
{
    public interface IHttpRequestAuthenticateService
    {
        Task<Result<string>> GetOAuthToken();
    }

    public sealed class HttpRequestAuthenticateService : IHttpRequestAuthenticateService
    {
        private readonly HttpClient _httpClient;

        public HttpRequestAuthenticateService()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ClientCertificates.Add(ClientCertificate.GetClientCertificate());
            _httpClient = new HttpClient(httpClientHandler);
        }

        public async Task<Result<string>> GetOAuthToken()
        {
            try
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(PixUrl.Url_Production_AuthenticateOAuth),
                    Method = HttpMethod.Post
                };

                var credencials = ClientCertificate.GetCredencials();
                var authorization = ClientCertificate.GetAuthorization(credencials);
                HttpContent httpContent = new StringContent("{\r\n    \"grant_type\": \"client_credentials\"\r\n}", Encoding.UTF8, "application/json");
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = httpContent;
                request.Headers.Add("Authorization", "Basic " + authorization);
                var response = await _httpClient.SendAsync(request);
                var stringResponse = await response.Content.ReadFromJsonAsync<Token>();
                if (!response.IsSuccessStatusCode)
                {
                    return Error.New("IsNotSuccessStatusCode", $"Status Code: {response.StatusCode}. Response: {response.RequestMessage}");
                }
                return Convert.ToString(stringResponse.access_token);
            }
            catch (Exception ex)
            {
                return Error.New("ErrorInternal", $"Error internal message: {ex.Message}");
            }
        }
    }
}
