const axios = require('axios');
const fs = require('fs');
const FormData = require('form-data');
const generateAccessToken = require('./generateAccessToken');

// Immediately-invoked async function
(async () => {
  // Call the function to generate access token
  const accessToken = await generateAccessToken();

  let jobId;
  let orgId;

  try {
    const orgsResponse = await axios.get('https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/config/organizations', {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });
    console.log(orgsResponse.data);
    
    orgId = orgsResponse.data.result[0].id;  // assuming orgId can be obtained like this
  } catch (error) {
    console.error(`Error in organization fetch: ${error}`);
  }

  try {
    // Step 1: GET - Get the list of Equipment Activities
    const activitiesResponse = await axios.get(`https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/tasks/${orgId}/equipmentactivities`, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });
  
    console.log(JSON.stringify(activitiesResponse.data, null, 2));
    // Process the data received in the response
  
  } catch (error) {
    console.error(`Error in Step 1 - GET Equipment Activities: ${error}`);
    if (error.response) {
      console.error(`Response data: ${JSON.stringify(error.response.data)}`);
    }
  }
})();