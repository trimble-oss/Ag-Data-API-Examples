using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SampleCode.DataTransferObjects;

namespace SampleClientCode
{
    public class PrescriptionsClient
    {
        private readonly string _prescriptionsApiUrl;
        private readonly string _orgId;
        private readonly string _accessToken;

        public PrescriptionsClient(string accessToken, string prescriptionsApiUrl, string? orgId)
        {
            _prescriptionsApiUrl = prescriptionsApiUrl;
            _orgId = orgId;
            _accessToken = accessToken;
        }

        public async Task SendPrescriptions()
        {
            //Initiate Prescription import
            var prescriptionsApiUrl = _prescriptionsApiUrl + "/" + _orgId + "/rx/importjob";
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            
            var prescription = new Prescription()
            {
                FileName = "PrescriptionExample_S1",
                RateColumn = "Rate",
                RateUnit = "kg/ha",
                DeviceIds = null
            };
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            var prescriptionJsonString = JsonSerializer.Serialize(prescription, options);
            var prescriptionData = new StringContent(prescriptionJsonString, Encoding.UTF8, "application/json");

            var createdPrescription = await httpClient.PostAsync(prescriptionsApiUrl, prescriptionData);

            var jsonResponseString = await createdPrescription.Content.ReadAsStringAsync();

            var prescriptionElement = JsonDocument.Parse(jsonResponseString).RootElement;

            var jobIdExist = prescriptionElement.TryGetProperty("jobId", out JsonElement jobId);

            if (jobIdExist)
            {
                Console.WriteLine("\n\nThis is the id of the job that just got created which will be passed in the subsequent PATCH request : " + jobId.GetString());
            }
            else
            {
                Console.WriteLine("\n\nFailed to send the prescription.");
            }

            //Upload zip file for Prescription import
            prescriptionsApiUrl = prescriptionsApiUrl + "/" + jobId;

            await using var stream = File.OpenRead("../../../Files/Prescription_ShapeFile_20220316_155414.zip");

            using var request = new HttpRequestMessage(HttpMethod.Patch, prescriptionsApiUrl);

            request.Content = new StreamContent(stream);

            await httpClient.SendAsync(request);

            Console.WriteLine("\n\nPrescription Import Completed.");
        }
    }
}