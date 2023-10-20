using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SampleCode.DataTransferObjects;

namespace SampleClientCode
{
    public class FieldsClient
    {
        private readonly string _resourcesApiUrl;
        private readonly string _orgId;
        private readonly string _accessToken;

        public FieldsClient(string accessToken, string resourcesApiUrl, string? orgId)
        {
            _resourcesApiUrl = resourcesApiUrl;
            _orgId = orgId;
            _accessToken = accessToken;
        }

        public async Task<string> SendField()
        {
            var fieldApiUrl = _resourcesApiUrl + "/" + _orgId +  "/fields";
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            //change the field name every time you make a request as the filed api does not allow duplicate names
            var fieldToCreate = new Field()
            {
                Name = "Some Test Field18",
                Area = 50
            };

            var fieldJsonString = JsonSerializer.Serialize(fieldToCreate);
            var fieldData = new StringContent(fieldJsonString, Encoding.UTF8, "application/json");

            var createdField = await httpClient.PostAsync(fieldApiUrl, fieldData);

            var jsonResponseString = await createdField.Content.ReadAsStringAsync();

            var fieldElement = JsonDocument.Parse(jsonResponseString).RootElement;
            var idExist = fieldElement.TryGetProperty("id", out JsonElement fieldId);

            if (idExist)
            {
                Console.WriteLine("This is the id of the field that just got created: " + fieldId.GetString());
            }
            else
            {
                Console.WriteLine("\n\nFailed to send the field . Probably a duplicate field name was passed. Please try again.");
            }

            return fieldId.GetString();
        }
    }
}