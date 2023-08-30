const axios = require('axios');
const fs = require('fs');
const FormData = require('form-data');
const generateAccessToken = require('./generateAccessToken');

// Function to fetch boundaries
const fetchBoundaries = async (orgId, accessToken) => {
    try {
        const headers = {
            Authorization: `Bearer ${accessToken}`,
            'Content-Type': 'application/json'
        };

        const endpoint = `https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/resources/${orgId}/boundaries?includeLinks=true`;
        
        const response = await axios.get(endpoint, { headers: headers });
        console.log(response.data);
        return response.data;
    } catch (error) {
        console.error(`Error fetching boundaries: ${error}`);
        if (error.isAxiosError && error.response) {
            console.error(`Boundary fetch response data: ${JSON.stringify(error.response.data)}`);
        }
    }
};

// Immediately-invoked async function
(async () => {
    // Call the function to generate access token
    const accessToken = await generateAccessToken();

    let orgId;

    try {
        const orgsResponse = await axios.get('https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/config/organizations', {
            headers: {
                Authorization: `Bearer ${accessToken}`
            }
        });
        console.log(orgsResponse.data);
        
        orgId = orgsResponse.data.result[0].id;
    } catch (error) {
        console.error(`Error in organization fetch: ${error}`);
    }

    // Fetch the list of boundaries for the orgId
    await fetchBoundaries(orgId, accessToken);
})();
