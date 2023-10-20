using System.Net.Http.Headers;
using System.Text.Json;

namespace SampleClientCode
{
    public class AsAppliedDataClient
    {
        private readonly string _asAppliedApiUrl;
        private readonly string? _orgId;
        private readonly string _accessToken;

        public AsAppliedDataClient(string accessToken, string asAppliedApiUrl, string? orgId)
        {
            _asAppliedApiUrl = asAppliedApiUrl;
            _orgId = orgId;
            _accessToken = accessToken;
        }

        public async Task GetAsAppliedData()
        {
            //GET  Equipment Activities
            var equipmentActivityApiUrl = _asAppliedApiUrl + "/" + _orgId + "/" + "/equipmentactivities?includelinks=false";
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var equipmentActivityResponse = await httpClient.GetAsync(equipmentActivityApiUrl);

            var jsonResponseString = await equipmentActivityResponse.Content.ReadAsStringAsync();

            //Get the first one from the list
            var firstEquipmentActivity = JsonDocument.Parse(jsonResponseString).RootElement.GetProperty("result")[0];
            var equipmentActivityId = firstEquipmentActivity.GetProperty("id").GetString();


            //GET Layers
            //Use the first equipmentActivity that was retrieved from GET Equipment Activities
            var layerApiUrl = _asAppliedApiUrl + "/" + _orgId + "/layers" + "?equipmentactivityId=" + equipmentActivityId;

            var layerResponse = await httpClient.GetAsync(layerApiUrl);

            var layersJsonString = await layerResponse.Content.ReadAsStringAsync();

            var layerElement = JsonDocument.Parse(layersJsonString).RootElement[0];
            var layerId = layerElement.GetProperty("id").GetString();


            //GET Layer Samples
            var layerSamplesApiUrl = _asAppliedApiUrl + "/" + _orgId + "/layers/" + layerId + "/samples";

            var layerSampleResponse = await httpClient.GetAsync(layerSamplesApiUrl);

            var layerSamplesJsonString = await layerSampleResponse.Content.ReadAsStringAsync();
            
            Console.WriteLine("This is the GET Layer Samples response string in json format :\n\n" + layerSamplesJsonString + "\n\n");
        }
    }
}