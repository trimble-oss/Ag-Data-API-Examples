package sampleCode;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.net.http.HttpResponse.BodyHandlers;

import org.json.JSONArray;
import org.json.JSONObject;

public class AsAppliedData {
	
	static String token = GetAuthToken.token;
	static String orgId = GetOrgs.orgId;
	
	//Method to get As-applied Data
	public static void asAppliedData() {
		
		String equipmentActivityId = null;
		String layerId = null;
		HttpClient client = HttpClient.newHttpClient();
 		
		//GET Equipment Activities
		HttpRequest request = HttpRequest.newBuilder()
				.uri(URI.create("https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/tasks/"+orgId+"/equipmentactivities?includelinks=false"))
				.header ("Authorization", "Bearer " + token)
				.GET()
				.build();
		
		try {
			HttpResponse<String> response = client.send(request, BodyHandlers.ofString());
			
			JSONObject root = new JSONObject(response.body());
	        equipmentActivityId = root.getJSONArray("result").getJSONObject(2).getString("id");
	        
		} catch (IOException e) {
			System.out.println(e.getMessage());
			e.printStackTrace();
		} catch (InterruptedException e) {
			System.out.println(e.getMessage());
			e.printStackTrace();
		}
		
		//GET Layers
		HttpRequest newRequest = HttpRequest.newBuilder()
				.uri(URI.create("https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/tasks/"+orgId+"/layers?equipmentActivityId="+equipmentActivityId))
				.header ("Authorization", "Bearer " + token)
		        .GET()
		        .build();
		
		try {
			HttpResponse<String> response = client.send(newRequest, BodyHandlers.ofString());
			
			JSONArray root = new JSONArray(response.body());
			JSONObject id = (JSONObject) root.get(0);
			layerId = id.getString("id");
			
		} catch (IOException e) {
			System.out.println(e.getMessage());
			e.printStackTrace();
		} catch (InterruptedException e) {
			System.out.println(e.getMessage());
			e.printStackTrace();
		}
		
		
		//GET Layer Samples
		HttpRequest finalRequest = HttpRequest.newBuilder()
				.uri(URI.create("https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/tasks/"+orgId+"/layers/"+layerId+"/samples"))
				.header ("Authorization", "Bearer " + token)
		        .GET()
		        .build();
		
		try {
			HttpResponse<String> response = client.send(finalRequest, BodyHandlers.ofString());
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
