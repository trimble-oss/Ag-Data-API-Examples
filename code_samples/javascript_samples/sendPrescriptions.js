const axios = require('axios');
const fs = require('fs');
const generateAccessToken = require('./generateAccessToken');

// Immediately-invoked async function
(async () => {
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
    
    orgId = orgsResponse.data.result[0].id;
  } catch (error) {
    console.error(`Error in organization fetch: ${error}`);
  }

  try {
    const prescriptionResponse = await axios.post(`https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/prescriptions/${orgId}/rx/importjob`, {
      fileName: "/Users/hbarnes/Downloads/Ag_Data_API_Project/PrescriptionFileExample2.zip",
      rateColumn: "0-60-0",
      rateUnit: "gal/ac",
      deviceIds: null
    }, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });

    console.log(prescriptionResponse.data);
    jobId = prescriptionResponse.data.jobId;
  } catch (error) {
    console.error(`Error in Step 1: ${error}`);
    if (error.response) {
      console.error(`Response data: ${JSON.stringify(error.response.data)}`);
    }
  }

  try {
    const fileStream = fs.createReadStream('/Users/hbarnes/Downloads/Ag_Data_API_Project/PrescriptionFileExample2.zip');

    const uploadResponse = await axios.patch(`https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/prescriptions/${orgId}/rx/importjob/${jobId}`, fileStream, {
      headers: {
        'Authorization': `Bearer ${accessToken}`,
        'Content-Type': 'application/zip'
      }
    });

    if (uploadResponse.status === 204) {
      console.log('Upload successful.');
    } else {
      console.log('Upload unsuccessful.');
    }
  } catch (error) {
    console.error(`Error in Step 2: ${error}`);
    if (error.response) {
      console.error(`Response data: ${JSON.stringify(error.response.data)}`);
    }
  }

  try {
    await new Promise(resolve => setTimeout(resolve, 1000));

    const importStatusResponse = await axios.get(`https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/prescriptions/${orgId}/rx/importjob/${jobId}`, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      }
    });

    console.log(importStatusResponse.data);

    const { status, rxFileInfo } = importStatusResponse.data;
    console.log(rxFileInfo);
    const { jobId: rxJobId, status: rxStatus } = rxFileInfo;  
    console.log(rxJobId);
    console.log(rxStatus);
    
    const sendStatusResponse = await axios.get(`https://cloud.api.trimble.com/Trimble-Ag-Software/externalApi/3.0/prescriptions/${orgId}/rx/${jobId}`, {
      headers: {
        Authorization: `Bearer ${accessToken}`
      },
      params: {
        includeLinks: true
      }
    });

    console.log(sendStatusResponse.data);
  } catch (error) {
    console.error(`Error in Step 3 or 4: ${error}`);
    if (error.response) {
      console.error(`Response data: ${JSON.stringify(error.response.data)}`);
    }
  }
})();
