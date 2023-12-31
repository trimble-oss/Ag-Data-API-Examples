# Java Samples/Examples
This folder contains Java code samples for the common workflows given in the Ag Data API documentation.

## Installation & Dependencies

1. Clone the repository.
`git clone https://github.com/trimble-oss/Ag-Data-API-Examples.git`
2. Install [Java](https://www.java.com/en/download/help/download_options.html) and [Maven](https://maven.apache.org/install.html). This project uses the following versions of Java and Maven.
  * Java version "20.0.1".
  * Maven version "apache-maven-3.9.2".
3. You can add appropriate dependencies to pom.xml depending on your versions of Java and Maven
4. You may need to set your `JAVA_HOME`

## Running the project

1. The main method is in the `SampleCodeMain.java` file.
2. You can comment out the method calls that you do not wish to execute in the main method.
3. `GetAuthToken.java` class generates the access token for your application. This code uses Client Credentials. You will have to change the code if your application uses an Authorization Code Grant.
4. You will have to add the API credentials for your application in `SampleCodeMain.java` file and your application name in `GetAuthToken.java` file to generate an access token successfully.
5. You can run the project in either command line/terminal or by using an IDE.

## Sample Prescription Files

1. Sample prescription files are under the [files](/files/prescriptions) folder.
2. You will find three files with Rate Column and Rate Unit values as below
  * Test_Rx1.zip
    * Rate Column - 0-52-0
    * Rate Unit - lbs/ac
  * Test_Rx2.zip
    * Rate Column - 0-0-15
    * Rate Unit - lbs/ac
  * Test_Rx3.zip
    * Rate Column - 11-52-0
    * Rate Unit - lbs/ac

## Get Access to Trimble Ag Data APIs

1. Request access to Trimble Ag Data APIs by completing [this](https://agriculture.trimble.com/en/partners/developer-resources/request-software-integration-api) form online.
2. You can find the API Documentation [here](https://agdeveloper.trimble.com/api-docs).
3. Please send your API related queries to our support email ag_api@trimble.com.