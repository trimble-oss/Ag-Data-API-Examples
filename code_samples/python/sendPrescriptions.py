import requests
import time
from generateAccessToken import generate_access_token

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

    # Step 1: Create prescription import job
    job_id = None
    try:
        prescription_data = {
            'fileName': "/workspaces/Ag-Data-API-Examples/code_samples/javascript_samples/PrescriptionFileExample2.zip",
            'rateColumn': "0-60-0",
            'rateUnit': "gal/ac",
            'deviceIds': None
        }
        prescription_response = requests.post(f'https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/prescriptions/{org_id}/rx/importjob', json=prescription_data, headers=headers)
        print(prescription_response.json())
        
        job_id = prescription_response.json()['jobId']
    except Exception as error:
        print(f"Error in Step 1: {error}")
        if hasattr(error, 'response') and error.response:
            print(f"Response data: {json.dumps(error.response.json())}")

    # Step 2: Upload prescription file
    try:
        # Open the file in binary mode and read its content
        with open('/workspaces/Ag-Data-API-Examples/code_samples/javascript_samples/PrescriptionFileExample2.zip', 'rb') as file:
            file_data = file.read()

        upload_response = requests.patch(f'https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/prescriptions/{org_id}/rx/importjob/{job_id}', data=file_data, headers={
            'Authorization': f'Bearer {access_token}',
            'Content-Type': 'application/zip'
        })

        if upload_response.status_code == 204:
            print('Upload successful.')
        else:
            print('Upload unsuccessful.')
    except Exception as error:
        print(f"Error in Step 2: {error}")
        if hasattr(error, 'response') and error.response:
            print(f"Response data: {json.dumps(error.response.json())}")

   # Step 3 and 4: Check import status
    try:
        time.sleep(3)  # Wait for a few seconds

        headers = {
            'Authorization': f'Bearer {access_token}'
        }

        import_status_response = requests.get(f'https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/prescriptions/{org_id}/rx/importjob/{job_id}', headers=headers)
        import_status_data = import_status_response.json()
        
        # Print the import_status_data to understand its structure
        print("Import Status Data:", import_status_data)

        # If import_status_data is a dictionary and has the key 'rxFileInfo'
        if isinstance(import_status_data, dict) and 'rxFileInfo' in import_status_data:
            rx_file_info = import_status_data['rxFileInfo']
            if rx_file_info:  # Check if the list is not empty
                rx_name = rx_file_info[0]['rxName']
                rx_id = rx_file_info[0]['rxId']
                error_code = rx_file_info[0].get('errorCode', None)  # Using get() to avoid KeyError
                print("RX Name:", rx_name)
                print("RX ID:", rx_id)
                print("Error Code:", error_code)
            else:
                print("rxFileInfo list is empty.")
        else:
            print("Unexpected structure for import_status_data")

    except Exception as error:
        print(f"Error in Step 3 or 4: {error}")
        if hasattr(error, 'response') and error.response:
            print(f"Response data: {json.dumps(error.response.json())}")

if __name__ == "__main__":
    main()