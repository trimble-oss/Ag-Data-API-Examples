using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;
using SampleCode.DataTransferObjects;

namespace SampleClientCode
{
    public class BoundariesClient
    {
        private readonly string _resourcesApiUrl;
        private readonly string _orgId;
        private readonly string _accessToken;
        private readonly string _fieldId;

        public BoundariesClient(string accessToken, string resourcesApiUrl, string? orgId, string fieldId)
        {
            _resourcesApiUrl = resourcesApiUrl;
            _orgId = orgId;
            _accessToken = accessToken;
            _fieldId = fieldId;
        }

        public async Task<string> SendBoundary()
        {
            var boundariesApiUrl = _resourcesApiUrl + "/" + _orgId + "/boundaries";

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            // Add your boundary info here
            var boundaryToCreate = new Boundary()
            {
                Name = "Some Test Boundary15",
                FieldId = new Guid(_fieldId),
                Geometry = new GeometryFactory().CreatePolygon(new Coordinate[]
                {
                new Coordinate(-50.92206440928118,16.73240259727149),
                new Coordinate(-50.92207997507916,16.732415410798335),
                new Coordinate(-50.9226155938253,16.732386766217115),
                new Coordinate(-50.92254769623408,16.72547564102975),
                new Coordinate(-50.92196563143528,16.72546431234128),
                new Coordinate(-50.92206440928118,16.73240259727149)
                })
            };

            var geoJsonConverter = new NetTopologySuite.IO.Converters.GeoJsonConverterFactory();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true

            };

            options.Converters.Add(geoJsonConverter);

            var boundaryJsonString = JsonSerializer.Serialize(boundaryToCreate, options);
            var boundaryData = new StringContent(boundaryJsonString, Encoding.UTF8, "application/json");

            var createdBoundary = await httpClient.PostAsync(boundariesApiUrl, boundaryData);

            var jsonResponseString = await createdBoundary.Content.ReadAsStringAsync();

            var boundaryElement = JsonDocument.Parse(jsonResponseString).RootElement;

            var idExist = boundaryElement.TryGetProperty("id", out JsonElement boundaryId);

            if (idExist)
            {
                Console.WriteLine("\n\nThis is the id of the boundary that just got created: " + boundaryId.GetString());
            }
            else
            {
                Console.WriteLine("\n\nFailed to send the boundary. Please try again.");
            }

            return boundaryId.GetString();
        }
    }
}