const axios = require('axios');
const fs = require('fs');
const FormData = require('form-data');
const generateAccessToken = require('./generateAccessToken');

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
    
    // EDIT HERE: Choose the desired activity by its index in the array.
    // For example, to get the first activity, set activityIndex to 0.
    // To get the second activity, set activityIndex to 1, and so on.
    const activityIndex = 0; // <--- EDIT THIS LINE TO CHOOSE ACTIVITY ID
    const equipmentActivityId = activitiesResponse.data.result[activityIndex].id;

    // Step 2: GET - Get the list of Layers for an Equipment Activity ID
    const layersResponse = await axios.get(`https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/tasks/${orgId}/layers`, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      },
      params: {
        equipmentActivityId: equipmentActivityId
      }
    });
    
    console.log(JSON.stringify(layersResponse.data, null, 2));

    // Assuming the first layer's ID is what we need for the next step
    const layerId = layersResponse.data[0].id;

    // Step 3: GET - Get the list of Samples on a Layer
    const samplesResponse = await axios.get(`https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/tasks/${orgId}/layers/${layerId}/samples`, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });

    console.log(JSON.stringify(samplesResponse.data, null, 2));

  } catch (error) {
    console.error(`Error in Steps: ${error}`);
    if (error.response) {
      console.error(`Response data: ${JSON.stringify(error.response.data)}`);
    }
  }
})();