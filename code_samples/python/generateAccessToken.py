import requests
import base64

def generate_access_token():
    try:
        client_id = 'your client ID here'  # Add your client ID here
        client_secret = 'your client secret here'  # Add your client secret here (this should be stored securely)

        headers = {
            'Authorization': f'Basic {base64.b64encode(f"{client_id}:{client_secret}".encode()).decode()}',
            'Content-Type': 'application/x-www-form-urlencoded'
        }

        data = {
            'grant_type': 'client_credentials',
            'scope': 'ShivaniClient-Test'
        }

        response = requests.post('https://id.trimble.com/oauth/token', data=data, headers=headers)

        print(response.json())
        return response.json().get('access_token')
    except Exception as error:
        print(f"Error: {error}")

if __name__ == "__main__":
    generate_access_token()
