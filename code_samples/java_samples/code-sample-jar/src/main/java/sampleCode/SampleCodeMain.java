package sampleCode;

public class SampleCodeMain {

	public static void main(String[] args) {
		GetAuthToken.getAuthTokenClientCreds("YOUR_CLIENT_ID","YOUR_CLIENT_SECRET"); //Add your Client IDs and Secret here//
		
		GetOrgs.getOrgs();
		
		AsAppliedData.asAppliedData();
		
		SendFields.sendFields();
		
		SendBoundaries.sendBoundaries();
		
		SendGuidanceLines.sendGuidanceLines();
		
		SendPrescriptions.sendPrescriptions();
	}

}
