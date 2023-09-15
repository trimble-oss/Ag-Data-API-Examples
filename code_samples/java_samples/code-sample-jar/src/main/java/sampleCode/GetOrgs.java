package sampleCode;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.net.http.HttpResponse.BodyHandlers;

import org.json.JSONObject;

public class GetOrgs {
	
	static String token = GetAuthToken.token;
	public static String orgId = null;
	
	//Method to get all authorized Orgs
	public static String getOrgs() {
		
		HttpRequest request = HttpRequest.newBuilder()
				.uri(URI.create("https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/config/organizations"))
				.header ("Authorization", "Bearer " + token)
				.GET()
				.build();
		
		HttpClient client = HttpClient.newHttpClient();
		
		try {
			HttpResponse<String> response = client.send(request, BodyHandlers.ofString());
			JSONObject root = new JSONObject(response.body());
	        orgId = root.getJSONArray("result").getJSONObject(0).getString("id");
	        
		} catch (IOException e) {
			System.out.println(e.getMessage());
			e.printStackTrace();
		} catch (InterruptedException e) {
			System.out.println(e.getMessage());
			e.printStackTrace();
		}
		
		return orgId;
	}
}
