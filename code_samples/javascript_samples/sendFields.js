const axios = require('axios');
const fs = require('fs');
const FormData = require('form-data');
const generateAccessToken = require('./generateAccessToken');

// Function to send fields to Trimble Ag Software
const sendFieldsToTrimbleAgSoftware = async (orgId, fieldsData, accessToken) => {
    try {
        const headers = {
            Authorization: `Bearer ${accessToken}`,
            'Content-Type': 'application/json'
        };
        const response = await axios.post(`https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/resources/${orgId}/fields/`, fieldsData, { headers: headers });
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

    try {
        const fieldsData = {
            name: 'APItry2', // Example field name
            area: 0 // Example field area
        };
        sendFieldsToTrimbleAgSoftware(orgId, fieldsData, accessToken);
    } catch (error) {
        console.error(`Error in sending fields: ${error}`);
        if (error.response) {
            console.error(`Response data: ${JSON.stringify(error.response.data)}`);
        }
    }
})();
