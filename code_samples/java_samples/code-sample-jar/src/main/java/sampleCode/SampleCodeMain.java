package sampleCode;

public class SampleCodeMain {

	public static void main(String[] args) {
		GetAuthToken.getAuthTokenClientCreds("YOUR_CLIENT_ID","YOUR_CLIENT_SECRET");
		
		GetOrgs.getOrgs();
		
		AsAppliedData.asAppliedData();
		
		SendFields.sendFields();
		
		SendBoundaries.sendBoundaries();
		
		SendGuidanceLines.sendGuidanceLines();
		
		SendPrescriptions.sendPrescriptions();
	}

}
