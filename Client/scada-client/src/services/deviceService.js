import axios from 'axios';

const API_BASE_URL = 'http://localhost:5045/api/Device';



async function getAllDevices() {
    const response = await axios.get(`${API_BASE_URL}`, {
      withCredentials: true
    });
    return response.data;
  }

  async function deleteDevice(id) {
    const response = await axios.delete(`${API_BASE_URL}/delete/`+id, {
      withCredentials: true
    });
    return response.data;
  }
  async function addDevice(device) {
    const response = await axios.post(`${API_BASE_URL}/device`,device, {
      withCredentials: true
    });
    return response.data;
  }
export {getAllDevices,deleteDevice,addDevice}