using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SampleCode.DataTransferObjects;

namespace SampleClientCode
{
    public class AccessTokenClient
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _clientName;
        private readonly string _apiCloudLoginUrl;

        public AccessTokenClient(string clientId, string clientSecret, string clientName, string apiCloudLoginUrl)
        {
            _clientSecret = clientSecret;
            _clientId = clientId;
            _clientName = clientName;
            _apiCloudLoginUrl = apiCloudLoginUrl;
        }

        public async Task<LoginToken> GetAccessTokenUsingClientCredentials()
        {
            var credentials = _clientId + ":" + _clientSecret;

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials)));

            var response = await httpClient.PostAsync(_apiCloudLoginUrl, new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"},
                {"scope", _clientName}
            }));

            var jsonResponseString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var loginToken = JsonSerializer.Deserialize<LoginToken>(jsonResponseString, options);

            return loginToken;
        }
    }
}