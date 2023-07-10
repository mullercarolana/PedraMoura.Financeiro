using ApplicationCore.Shared.Certificates;
using ApplicationCore.Shared.Pix;
using ApplicationCore.Shared.Urls;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PixController : ControllerBase
    {
        [HttpPost("Payment")]
        public IActionResult PixPayment([FromHeader] string authorization, Body body)
        {
            var certificates = ClientCertificate.GetClientCertificates();
            var options = new RestClientOptions(baseUrl: PixUrl.Url_Production_GeneratePix) { ClientCertificates = certificates };

            var request = new RestRequest();
            request.AddHeader("Authorization", "Bearer " + authorization);
            request.AddHeader("Content-Type", "application/json");
            request.AddBody(body);

            var client = new RestClient(options);
            var restResponse = client.ExecutePost(request);
            return restResponse.IsSuccessful 
                ? Ok(restResponse.Content) 
                : BadRequest(restResponse.Content);
        }
    }
}
