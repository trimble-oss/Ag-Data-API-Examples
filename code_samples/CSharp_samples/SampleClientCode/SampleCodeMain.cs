namespace SampleClientCode
{
    public class SampleCodeMain
    {
        //ClientId, ClientSecret and ClientName for the registered app
        public const string ClientId = "c3a2ad47-9bf3-4c00-b6bc-2b4ab3213bb0";
        public const string ClientSecret = "a6f4d553df914b8dbf3b6f758df932d7";
        public const string ClientName = "ShivaniClient-Test";

        //These URLs are for Production endpoints.They will be different for other environments.
        public const string ApiCloudLoginUrl = "https://id.trimble.com/oauth/token";
        public const string OrgApiUrl = "https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/config/organizations";

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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Message :" + ex.Message + "." + " Exception Stack Trace :" + ex.StackTrace );
                Console.ReadKey();
            }
        }
    }
}