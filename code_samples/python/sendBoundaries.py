import requests
import json
from generateAccessToken import generate_access_token

def send_boundary_to_trimble_ag_software(org_id, boundary_data, access_token):
    try:
        headers = {
            'Authorization': f'Bearer {access_token}',
            'Content-Type': 'application/json'
        }
        response = requests.post(f'https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/resources/{org_id}/boundaries/', json=boundary_data, headers=headers)
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

    # Send boundary data
    try:
        boundary_data = {
            'name': 'APItry1',
            'fieldId': '86d2a096-2d91-49c2-83ed-527e92675c46',
            'geometry': {
                'type': 'Polygon',
                'coordinates': [
                    [
                        [-104.99404, 39.73924],  # Point 1: Denver, CO
                        [-104.99404, 39.83924],  # Point 2: North of Denver, CO
                        [-105.09404, 39.83924],  # Point 3: North-west of Denver, CO
                        [-105.09404, 39.73924],  # Point 4: West of Denver, CO
                        [-104.99404, 39.73924]   # Point 5: Closing the polygon by repeating Point 1
                    ]
                ]
            }
        }
        send_boundary_to_trimble_ag_software(org_id, boundary_data, access_token)
    except Exception as error:
        print(f"Error in sending boundary: {error}")
        if hasattr(error, 'response') and error.response:
            print(f"Response data: {json.dumps(error.response.json())}")

if __name__ == "__main__":
    main()
