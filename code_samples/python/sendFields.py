import requests
import json
from generateAccessToken import generate_access_token

def send_fields_to_trimble_ag_software(org_id, fields_data, access_token):
    try:
        headers = {
            'Authorization': f'Bearer {access_token}',
            'Content-Type': 'application/json'
        }
        response = requests.post(f'https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/resources/{org_id}/fields/', json=fields_data, headers=headers)
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

    # Send fields data
    try:
        fields_data = {
            'name': 'APItry2',  # Example field name
            'area': 0  # Example field area
        }
        send_fields_to_trimble_ag_software(org_id, fields_data, access_token)
    except Exception as error:
        print(f"Error in sending fields: {error}")
        if hasattr(error, 'response') and error.response:
            print(f"Response data: {json.dumps(error.response.json())}")

if __name__ == "__main__":
    main()
