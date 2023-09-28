const axios = require('axios');

// Function to generate access token
const generateAccessToken = async () => {
    try {
        const clientId = 'Add your client ID here'; // Add your client ID here
        const clientSecret = 'Add your client secret here'; // Add your client secret here (this should be stored securely)
    
        const headers = {
          Authorization: `Basic ${Buffer.from(`${clientId}:${clientSecret}`).toString('base64')}`,
          'Content-Type': 'application/x-www-form-urlencoded'
        };
    
        const data = new URLSearchParams();
        data.append('grant_type', 'client_credentials');
        data.append('scope', 'Add your application name here'); // Add your application name here
    
        const response = await axios.post('https://id.trimble.com/oauth/token', data, {
          headers: headers,
        });
    
        console.log(response.data);
        return response.data.access_token;
      } catch (error) {
        console.error(`Error: ${error}`);
      }
};

module.exports = generateAccessToken;