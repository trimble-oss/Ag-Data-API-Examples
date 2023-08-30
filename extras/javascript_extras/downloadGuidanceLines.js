const axios = require('axios');
const generateAccessToken = require('./generateAccessToken');

// Function to fetch guidance lines from Trimble Ag Software
const fetchGuidanceLines = async (orgId, accessToken) => {
    try {
        const headers = {
            Authorization: `Bearer ${accessToken}`,
            'Content-Type': 'application/json'
        };
        const endpoint = `https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/resources/${orgId}/guidancelines?includeLinks=true`;
        const response = await axios.get(endpoint, { headers: headers });
        console.log(response.data);
    } catch (error) {
        if (error.isAxiosError) {
            console.error(`AxiosError: ${error.message}`);
            if (error.response) {
                console.error(`Response data: ${JSON.stringify(error.response.data)}`);
            }
        } else {
            console.error(`Error: ${error}`);
        }
    }
};

// Immediately-invoked async function
(async () => {
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

    // Fetch the list of guidance lines for the orgId
    await fetchGuidanceLines(orgId, accessToken);
})();

