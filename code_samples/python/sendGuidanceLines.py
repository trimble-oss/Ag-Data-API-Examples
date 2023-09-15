import requests
import json
from generateAccessToken import generate_access_token

def create_guidance_line(org_id, guidance_line_data, access_token):
    try:
        headers = {
            'Authorization': f'Bearer {access_token}',
            'Content-Type': 'application/json'
        }
        response = requests.post(f'https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/resources/{org_id}/guidancelines/', json=guidance_line_data, headers=headers)
        print(response.json())
    except requests.RequestException as error:
        print(f"AxiosError: {error}")
        if error.response:
            print(f"Response data: {json.dumps(error.response.json())}")
    except Exception as error:
        print(f"Error: {error}")

def main():
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
        
        org_id = orgs_data['result'][0]['id']
    except Exception as error:
        print(f"Error in organization fetch: {error}")

    # Create guidance line
    try:
        guidance_line_data = {
            'fieldId': "189543f0-26e0-4553-8603-6dd54355da1f",
            'name': "ExampleGuidanceLine",
            'type': "AB",
            'origin': {
                'type': "Point",
                'coordinates': [-104.99404, 39.73924]  # Example: Denver, CO
            },
            'infillId': "fe5bc049-e206-4443-aa31-8a03772c9c62",
            'geometry': {
                'type': "Polygon",
                'coordinates': [
                    [
                        [-104.99404, 39.73924],  # Point 1: Denver, CO
                        [-104.99404, 39.83924],  # Point 2: North of Denver, CO
                        [-105.09404, 39.83924],  # Point 3: North-west of Denver, CO
                        [-105.09404, 39.73924],  # Point 4: West of Denver, CO
                        [-104.99404, 39.73924]   # Point 5: Closing the polygon by repeating Point 1
                    ]
                ]
            },
            'numberOfSwaths': 0
        }
        
        create_guidance_line(org_id, guidance_line_data, access_token)
    except Exception as error:
        print(f"Error in creating guidance line: {error}")
        if hasattr(error, 'response') and error.response:
            print(f"Response data: {json.dumps(error.response.json())}")

if __name__ == "__main__":
    main()
