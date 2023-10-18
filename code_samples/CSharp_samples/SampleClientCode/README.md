# C# Samples/Examples
This folder contains C# code samples for the common workflows given in the Ag Data API documentation.

## Installation & Dependencies

1. Clone the repository.
`git clone https://github.com/trimble-oss/Ag-Data-API-Examples.git`
2. Install .NET 6.0 SDK from https://dotnet.microsoft.com/en-us/download/dotnet/6.0 

## Running the project

1. The main method is in the `SampleCodeMain.cs` file.
2. You can comment out the method calls that you do not wish to execute in the main method.
3. `AccessTokenClient.cs` class generates the access token for your application. This code uses Client Credentials. You will have to change the code if your application uses an Authorization Code Grant.
4. You will have to add the API credentials for your application in `SampleCodeMain.cs` file to generate an access token successfully.
5. You can run the project in either command line/terminal or by using an IDE.

## Get Access to Trimble Ag Data APIs

1. Request access to Trimble Ag Data APIs by completing [this](https://agriculture.trimble.com/en/partners/developer-resources/request-software-integration-api) form online.
2. You can find the API Documentation [here](https://agdeveloper.trimble.com/api-docs).
3. Please send your API related queries to our support email ag_api@trimble.com.