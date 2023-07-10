namespace ApplicationCore.Shared.Urls
{
    public static class PixUrl
    {
        public static string Url_Production_AuthenticateOAuth = "https://api-pix.gerencianet.com.br/oauth/token";
        public static string Url_Production_GeneratePix = "https://api-pix.gerencianet.com.br/v2/cob";

        public static string Url_Internal_Pix_Controller_Payment = "http://localhost:7071/api/Pix/Payment";
    }
}
