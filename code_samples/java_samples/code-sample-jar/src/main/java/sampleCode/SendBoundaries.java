package sampleCode;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.net.http.HttpResponse.BodyHandlers;

public class SendBoundaries {
	
	static String token = GetAuthToken.token;
	static String orgId = GetOrgs.orgId;
	
	//Method to POST Boundaries
	public static void sendBoundaries() {
		
		HttpRequest request = HttpRequest.newBuilder()
				.uri(URI.create("https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/resources/"+orgId+"/boundaries"))
				.header ("Authorization", "Bearer " + token)
				.header ("Accept-Encoding", "gzip, deflate")
				.header ("Content-Type", "application/json")
				.header ("Accept", "application/json")
				.POST(HttpRequest.BodyPublishers.ofString("{\"fieldID\":\"YOUR_FIELD_ID\",\"name\":\"TestBoundary1\",\"geometry\": {\"type\":\"Polygon\",\"coordinates\": [[[-50.92206440928118,16.73240259727149],[-50.92207997507916,16.732415410798335],[-50.9226155938253,16.732386766217115],[-50.92254769623408,16.72547564102975],[-50.92196563143528,16.72546431234128],[-50.92206440928118,16.73240259727149]]]}}"))
				.build();
		
		HttpClient client = HttpClient.newHttpClient();
		
		try {
			HttpResponse<String> response = client.send(request, BodyHandlers.ofString());
			System.out.println(response.statusCode());
			System.out.println(response.body());
			
		} catch (IOException e) {
			System.out.println(e.getMessage());
			e.printStackTrace();
		} catch (InterruptedException e) {
			System.out.println(e.getMessage());
			e.printStackTrace();
		}
	}

}
