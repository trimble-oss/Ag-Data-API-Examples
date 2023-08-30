const axios = require('axios');
const fs = require('fs');
const FormData = require('form-data');
const generateAccessToken = require('./generateAccessToken');

// Function to create a guidance line in Trimble Ag Software
const createGuidanceLine = async (orgId, guidanceLineData, accessToken) => {
    try {
        const headers = {
            Authorization: `Bearer ${accessToken}`,
            'Content-Type': 'application/json'
        };
        const response = await axios.post(`https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/resources/${orgId}/guidancelines/`, guidanceLineData, { headers: headers });
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
        // Using the provided example for guidance line data
        const guidanceLineData = {
            fieldId: "189543f0-26e0-4553-8603-6dd54355da1f",
            name: "ExampleGuidanceLine",
            type: "AB",
            origin: {
                type: "Point",
                coordinates: [-104.99404, 39.73924]  // Example: Denver, CO
            },
            infillId: "fe5bc049-e206-4443-aa31-8a03772c9c62",
            geometry: {
                type: "Polygon",
                coordinates: [
                    [
                        [-104.99404, 39.73924],  // Point 1: Denver, CO
                        [-104.99404, 39.83924],  // Point 2: North of Denver, CO
                        [-105.09404, 39.83924],  // Point 3: North-west of Denver, CO
                        [-105.09404, 39.73924],  // Point 4: West of Denver, CO
                        [-104.99404, 39.73924]   // Point 5: Closing the polygon by repeating Point 1
                    ]
                ]
            },
            numberOfSwaths: 0
        };
        
        await createGuidanceLine(orgId, guidanceLineData, accessToken);
    } catch (error) {
        console.error(`Error in creating guidance line: ${error}`);
        if (error.response) {
            console.error(`Response data: ${JSON.stringify(error.response.data)}`);
        }
    }
})();
