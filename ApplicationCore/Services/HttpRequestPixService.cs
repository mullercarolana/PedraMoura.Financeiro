using ApplicationCore.Seedwork.Exceptions;
using ApplicationCore.Shared.Certificates;
using ApplicationCore.Shared.Pix;
using ApplicationCore.Shared.Urls;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace ApplicationCore.Services
{
    public interface IHttpRequestPixService
    {
        Task<Result<PixPayment>> GeneratePixPaymentAsync(string token, object inputModel);
    }

    public sealed class HttpRequestPixService : IHttpRequestPixService
    {
        private readonly HttpClient _httpClient;

        public HttpRequestPixService()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ClientCertificates.Add(ClientCertificate.GetClientCertificate());
            _httpClient = new HttpClient(httpClientHandler);
        }

        public async Task<Result<PixPayment>> GeneratePixPaymentAsync(string token, object inputModel)
        {
            try
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(PixUrl.Url_Internal_Pix_Controller_Payment),
                    Method = HttpMethod.Post
                };

                var body = JsonConvert.SerializeObject(inputModel);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                request.Headers.Add("authorization", token);
                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                if (!response.IsSuccessStatusCode)
                {
                    return Error.New("IsNotSuccessStatusCode", $"Response: {response.RequestMessage}");
                }
                var pixPayment = await response.Content.ReadFromJsonAsync<PixPayment>();
                return pixPayment;
            }
            catch (Exception ex)
            {
                return Error.New("ErrorInternal", $"Error internal message: {ex.Message}");
            }
        }
    }
}
