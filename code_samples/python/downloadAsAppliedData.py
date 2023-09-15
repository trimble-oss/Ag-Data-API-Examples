import requests
import json
from generateAccessToken import generate_access_token

def main():
    # Call the function to generate access token
    access_token = generate_access_token()

    org_id = None

    # Fetch organizations
    try:
        headers = {
            'Authorization': f'Bearer {access_token}'
        }
        orgs_response = requests.get('https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/config/organizations', headers=headers)
        orgs_data = orgs_response.json()
        print(orgs_data)
        
        org_id = orgs_data['result'][0]['id']  # assuming orgId can be obtained like this
    except Exception as error:
        print(f"Error in organization fetch: {error}")

    # Step 1: GET - Get the list of Equipment Activities
    try:
        activities_response = requests.get(f'https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/tasks/{org_id}/equipmentactivities', headers=headers)
        activities_data = activities_response.json()
        print(json.dumps(activities_data, indent=2))
        # Process the data received in the response
    except Exception as error:
        print(f"Error in Step 1 - GET Equipment Activities: {error}")
        if activities_response:
            print(f"Response data: {json.dumps(activities_response.json())}")

if __name__ == "__main__":
    main()
