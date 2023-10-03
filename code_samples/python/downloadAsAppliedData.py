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
        
        # EDIT HERE: Choose the desired activity by its index in the array.
        activity_index = 0
        equipment_activity_id = activities_data['result'][activity_index]['id']

        # Step 2: GET - Get the list of Layers for an Equipment Activity ID
        layers_response = requests.get(f'https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/tasks/{org_id}/layers?equipmentActivityId={equipment_activity_id}', headers=headers)
        layers_data = layers_response.json()
        print(json.dumps(layers_data, indent=2))

        layer_id = layers_data['result'][0]['id']

        # Step 3: GET - Get the list of Samples on a Layer
        samples_response = requests.get(f'https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/tasks/{org_id}/layers/{layer_id}/samples', headers=headers)
        samples_data = samples_response.json()
        print(json.dumps(samples_data, indent=2))

    except Exception as error:
        print(f"Error in Steps: {error}")
        if 'activities_response' in locals():
            print(f"Response data: {json.dumps(activities_response.json())}")

if __name__ == "__main__":
    main()

