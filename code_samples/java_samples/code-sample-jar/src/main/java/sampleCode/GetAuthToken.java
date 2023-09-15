package sampleCode;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse.BodyHandlers;
import java.nio.charset.StandardCharsets;
import java.util.Base64;
import java.util.regex.Matcher;
import java.util.regex.Pattern;


import java.net.http.HttpResponse;

public class GetAuthToken {
	
	public static String token = null;
	
	//Method to generate an authentication token using Client Credentials Grant
	public static String getAuthTokenClientCreds(String clientId, String clientSecret) {
		
		HttpClient client = HttpClient.newHttpClient();
		String credential = clientId + ":" + clientSecret;
		byte[] encodedCreds = Base64.getEncoder().encode(credential.getBytes(StandardCharsets.UTF_8));
		String authHeaderValue = "Basic " + new String(encodedCreds);
		
		HttpRequest request = HttpRequest.newBuilder()
				.uri(URI.create("https://id.trimble.com/oauth/token"))
				.header ("Authorization", authHeaderValue)
				.header ("Content-Type", "application/x-www-form-urlencoded")
				.POST(HttpRequest.BodyPublishers.ofString("grant_type=client_credentials&scope=YOUR_APPLICATION_NAME"))
				.build();
		
		try {
			HttpResponse<String> response = client.send(request, BodyHandlers.ofString());
			
			Pattern p = Pattern.compile("\"access_token\":\\s*(.*)");
			Matcher m = p.matcher(response.body());		
			
			if(m.find()) {
				String group = m.group();
				token = group.substring(16,group.length()-1).replaceAll("\"", "");		
			}
			else {
				throw new IOException ("Access Token Not Found");
			}
			
		} catch (IOException e) {
			System.out.println(e.getMessage());
			e.printStackTrace();
		} catch (InterruptedException e) {
			System.out.println(e.getMessage());
			e.printStackTrace();
		}

		return token;
	}	
}
