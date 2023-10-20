using System.Net.Http.Headers;
using System.Text.Json;
using SampleCode.DataTransferObjects;

namespace SampleClientCode
{
    public class OrganizationsClient
    {
        private string _accessToken;
        private string _organizationApiUrl;

        public OrganizationsClient(string accessToken, string organizationApiUrl)
        {
            _accessToken = accessToken;
            _organizationApiUrl = organizationApiUrl;
        }

        public async Task<List<Organization>> GetAuthorizedOrganizations()
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var response = await httpClient.GetAsync(_organizationApiUrl);

            var jsonResponseString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var actionResponse = JsonSerializer.Deserialize<ActionResponse<Organization>>(jsonResponseString, options);

            return actionResponse?.Result;
        }
    }
}