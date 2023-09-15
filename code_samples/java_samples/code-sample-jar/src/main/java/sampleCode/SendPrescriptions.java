package sampleCode;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpRequest.BodyPublishers;
import java.net.http.HttpResponse;
import java.net.http.HttpResponse.BodyHandlers;
import java.nio.file.Path;
import java.nio.file.Paths;

import org.json.JSONObject;

public class SendPrescriptions {

	static String token = GetAuthToken.token;
	static String orgId = GetOrgs.orgId;
	
	//Method to upload Prescription file
	public static void sendPrescriptions() {
		
		String jobId = null;
		Path filePath = Paths.get("PRESCRIPTION_FILE_PATH");
		HttpClient client = HttpClient.newHttpClient();
		
		//POST Prescription
		HttpRequest request = HttpRequest.newBuilder()
				.uri(URI.create("https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/prescriptions/"+orgId+"/rx/importjob/"))
				.header ("Authorization", "Bearer " + token)
				.header ("Content-Type", "application/json")
				.header ("Accept", "application/json")
				.POST(HttpRequest.BodyPublishers.ofString("{\"FileName\":\"PrescriptionFileExample2.zip\",\"RateColumn\":\"0-60-0\",\"RateUnit\":\"kg/ha\",\"DeviceIds\": null}"))
				.build();
		
		try {
			HttpResponse<String> response = client.send(request, BodyHandlers.ofString());
			
			JSONObject root = new JSONObject(response.body());
			jobId = root.getString("jobId");
			
			System.out.println(response.statusCode());
			System.out.println(jobId);
			
		} catch (IOException e) {
			System.out.println(e.getMessage());
			e.printStackTrace();
		} catch (InterruptedException e) {
			System.out.println(e.getMessage());
			e.printStackTrace();
		}
		
		try {
			
			//PATCH Prescriptions
			HttpRequest newRequest = HttpRequest.newBuilder()
					.uri(URI.create("https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/prescriptions/"+orgId+"/rx/importjob/"+jobId))
					.header ("Authorization", "Bearer " + token)
					.header ("Content-Type", "application/json")
					.header ("Accept", "application/json")
					.method("PATCH", BodyPublishers.ofFile(filePath))
					.build();
			
			HttpResponse<String> response = client.send(newRequest, BodyHandlers.ofString());
			
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
