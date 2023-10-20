namespace SampleClientCode
{
    public class SampleCodeMain
    {
        //ClientId, ClientSecret and ClientName for the registered app
        //public const string ClientId = "YOUR_CLIENT_ID";
        //public const string ClientSecret = "YOUR_CLIENT_SECRET";
        //public const string ClientName = "YOUR_CLIENT_NAME";

        public const string ClientId = "c3a2ad47-9bf3-4c00-b6bc-2b4ab3213bb0";
        public const string ClientSecret = "a6f4d553df914b8dbf3b6f758df932d7";
        public const string ClientName = "ShivaniClient-Test";

        //These URLs are for Production endpoints.They will be different for other environments.
        public const string ApiCloudLoginUrl = "https://id.trimble.com/oauth/token";
        public const string OrgApiUrl = "https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/config/organizations";
        public const string ResourcesApiUrl = "https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/resources";
        public const string AsAppliedDataApiUrl = "https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/tasks";
        public const string PrescriptionsApiUrl = "https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/prescriptions";

        //This is the main method which is the entry point for this sample client code 
        public static async Task Main(string[] args)
        {
            try
            {
                //1. First get the access token from Trimble API Cloud. The response will be of the type 'SampleCode.DataTransferObjects.LoginToken'
                var accessTokenClient = new AccessTokenClient(ClientId, ClientSecret, ClientName, ApiCloudLoginUrl);
                var accessToken = await accessTokenClient.GetAccessTokenUsingClientCredentials();

                //2. Then use the token to get the list of authorized organizations . The response will be of the type 'List<SampleCode.DataTransferObjectsOrganization>'
                var organizationsClient = new OrganizationsClient(accessToken.Access_Token, OrgApiUrl);
                var organizations = await organizationsClient.GetAuthorizedOrganizations();

                //3. Then use the token and the first organization in the list of organizations to print the list of as Applied data .
                var asAppliedDataClient = new AsAppliedDataClient(accessToken.Access_Token, AsAppliedDataApiUrl, organizations.FirstOrDefault()?.Id.ToString());
                await asAppliedDataClient.GetAsAppliedData();

                //4.Use the token and the first organization to create the field
                var fieldClient = new FieldsClient(accessToken.Access_Token, ResourcesApiUrl, organizations.FirstOrDefault()?.Id.ToString());
                var fieldId = await fieldClient.SendField();

                //5. Use the token, organization and field to create the Boundary of the field
                var boundaryClient = new BoundariesClient(accessToken.Access_Token, ResourcesApiUrl, organizations.FirstOrDefault()?.Id.ToString(), fieldId);
                await boundaryClient.SendBoundary();

                //6. Use the token, organization and field to create the GuidanceLine in the field
                var guidanceClient = new GuidanceLinesClient(accessToken.Access_Token, ResourcesApiUrl, organizations.FirstOrDefault()?.Id.ToString(), fieldId);
                await guidanceClient.SendGuidanceLines();

                //7. Use the token and organization to import the Prescription
                var prescriptionClient = new PrescriptionsClient(accessToken.Access_Token, PrescriptionsApiUrl, organizations.FirstOrDefault()?.Id.ToString());
                await prescriptionClient.SendPrescriptions();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Message :" + ex.Message + "." + " Exception Stack Trace :" + ex.StackTrace );
                Console.ReadKey();
            }
        }
    }
}