const axios = require('axios');
const fs = require('fs');
const FormData = require('form-data');
const generateAccessToken = require('./generateAccessToken');

// Function to send boundary to Trimble Ag Software
const sendBoundaryToTrimbleAgSoftware = async (orgId, boundaryData, accessToken) => {
    try {
        const headers = {
            Authorization: `Bearer ${accessToken}`,
            'Content-Type': 'application/json'
        };
        const response = await axios.post(`https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/resources/${orgId}/boundaries/`, boundaryData, { headers: headers });
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
        const boundaryData = { // Example of boundary data
            name: 'APItry1',
            fieldId: '86d2a096-2d91-49c2-83ed-527e92675c46',
            geometry: {
                type: 'Polygon',
                coordinates: [
                    [
                        [-104.99404, 39.73924],  // Point 1: Denver, CO
                        [-104.99404, 39.83924],  // Point 2: North of Denver, CO
                        [-105.09404, 39.83924],  // Point 3: North-west of Denver, CO
                        [-105.09404, 39.73924],  // Point 4: West of Denver, CO
                        [-104.99404, 39.73924]   // Point 5: Closing the polygon by repeating Point 1
                    ]
                ]
            }
        };
        sendBoundaryToTrimbleAgSoftware(orgId, boundaryData, accessToken);
    } catch (error) {
        console.error(`Error in sending boundary: ${error}`);
        if (error.response) {
            console.error(`Response data: ${JSON.stringify(error.response.data)}`);
        }
    }
})();