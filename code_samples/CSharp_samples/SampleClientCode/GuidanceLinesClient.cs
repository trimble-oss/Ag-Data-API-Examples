using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;
using SampleCode.DataTransferObjects;
using static SampleCode.DataTransferObjects.GuidanceLine;

namespace SampleClientCode
{
    public class GuidanceLinesClient
    {
        private readonly string _resourcesApiUrl;
        private readonly string _orgId;
        private readonly string _accessToken;
        private readonly string _fieldId;

        public GuidanceLinesClient(string accessToken, string resourcesApiUrl, string? orgId, string fieldId)
        {
            _resourcesApiUrl = resourcesApiUrl;
            _orgId = orgId;
            _accessToken = accessToken;
            _fieldId = fieldId;
        }

        public async Task<string> SendGuidanceLines()
        {
            var guidanceLinesApiUrl = _resourcesApiUrl + "/" + _orgId + "/guidancelines";
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            
            var guidanceLineToCreate = new GuidanceLine()
            {
                Name = "Some Guidance Line15",
                FieldId = new Guid(_fieldId),
                Type = PatternType.AB,
                InfillId = null,
                NumberOfSwaths = null,
                Geometry = new GeometryFactory().CreateLineString(new Coordinate[]
                {
                    new Coordinate(-80.98175304599994,26.783957370000053),
                    new Coordinate(-80.98199208799997,26.783956905000025),
                    new Coordinate(-50.9226155938253,16.732386766217115),
                    new Coordinate(-50.92254769623408,16.72547564102975),
                    new Coordinate(-50.92196563143528,16.72546431234128),
                    new Coordinate(-50.92206440928118,16.73240259727149)
                })
            };

            var geoJsonConverter = new NetTopologySuite.IO.Converters.GeoJsonConverterFactory();
            var stringEnumConverter = new JsonStringEnumConverter();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            options.Converters.Add(geoJsonConverter);
            options.Converters.Add(stringEnumConverter);

            var guidanceJsonString = JsonSerializer.Serialize(guidanceLineToCreate, options);
            var guidanceData = new StringContent(guidanceJsonString, Encoding.UTF8, "application/json");

            var createdGuidanceLine = await httpClient.PostAsync(guidanceLinesApiUrl, guidanceData);

            var jsonResponseString = await createdGuidanceLine.Content.ReadAsStringAsync();

            var guidanceElement = JsonDocument.Parse(jsonResponseString).RootElement;

            var idExist = guidanceElement.TryGetProperty("id", out JsonElement guidanceId);

            if (idExist)
            {
                Console.WriteLine("\n\nThis is the id of the guidance line that just got created: " + guidanceId.GetString());
            }
            else
            {
                Console.WriteLine("\n\nFailed to send the guidance line. Please try again.");
            }

            return guidanceId.GetString();
        }
    }
}