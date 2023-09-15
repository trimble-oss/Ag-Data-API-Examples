package sampleCode;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.net.http.HttpResponse.BodyHandlers;

public class SendGuidanceLines {

	static String token = GetAuthToken.token;
	static String orgId = GetOrgs.orgId;
	
	//Method to POST Guidance Lines
	public static void sendGuidanceLines() {
		
		HttpRequest request = HttpRequest.newBuilder()
				.uri(URI.create("https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/resources/"+orgId+"/guidancelines"))
				.header ("Authorization", "Bearer " + token)
				.header ("Content-Type", "application/json")
				.header ("Accept", "application/json")
				.POST(HttpRequest.BodyPublishers.ofString("{\"fieldId\":\"YOUR_FIELD_ID\",\"name\":\"TestGuidanceLine\",\"type\":\"AB\",\"infillId\": null,\"geometry\": {\"type\":\"LineString\",\"coordinates\": [[-80.98175304599994,26.783957370000053],[-80.98199208799997,26.783956905000025]]},\"numberOfSwaths\": null}"))
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
