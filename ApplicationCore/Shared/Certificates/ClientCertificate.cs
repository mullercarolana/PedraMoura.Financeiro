using System.Security.Cryptography.X509Certificates;

namespace ApplicationCore.Shared.Certificates
{
    public static class ClientCertificate
    {
        public const string Key = "d9006649-8711-41b8-af72-bd0400233df9";
        public const string Client_Id = "Client_Id_4b6e183ed72f0edfdb979893249ce1dc773fd268";
        public const string Client_Secret = "Client_Secret_f0592ec158d227893f5cb05cb7f025dffbe07a9b";
        public const string FileName = "C:\\Unisinos\\Pedramoura.Financeiro\\ApplicationCore\\Shared\\Certificates\\producao-468058-pix_dotnet.p12";

        public static X509Certificate2 GetClientCertificate()
        {
            var uidCert = new X509Certificate2(FileName, string.Empty);
            return new X509Certificate2(uidCert);
        }

        public static X509CertificateCollection GetClientCertificates()
        {
            var uidCert = new X509Certificate2(FileName, string.Empty);
            return new X509CertificateCollection() { uidCert };
        }

        public static Dictionary<string, string> GetCredencials()
        {
            return new Dictionary<string, string>{
                {"client_id", Client_Id},
                {"client_secret", Client_Secret}
            };
        }

        public static string GetAuthorization(Dictionary<string, string> credencials)
        {
            return Base64Encode(credencials["client_id"] + ":" + credencials["client_secret"]);
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
