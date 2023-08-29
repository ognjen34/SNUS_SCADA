import axios from 'axios';

const API_BASE_URL = 'http://localhost:5045/api/AnalogInput';
const API_BASE_URL_DIGITAL = 'http://localhost:5045/api/DigitalInput'

async function CreateAnalog(analogTagDTO) {
    const response = await axios.post(`${API_BASE_URL}`, {
      ...analogTagDTO
    }, {
      withCredentials: true
    });
  
    return response.data;
  }

  async function DeleteAnalog(id) {
    console.log(id);
    const response = await axios.delete(`${API_BASE_URL}`+'/'+id ,{
      withCredentials: true
    });
  
    return response.data;
  }

  async function CreateDigital(digitalTagDTO) {
    const response = await axios.post(`${API_BASE_URL_DIGITAL}`, {
      ...digitalTagDTO
    }, {
      withCredentials: true
    });
  
    return response.data;
  }

  async function DeleteDigital(id) {
    console.log(id);
    const response = await axios.delete(`${API_BASE_URL_DIGITAL}`+'/'+id ,{
      withCredentials: true
    });
  
    return response.data;
  }


  async function UpdateDigital(newDigitalData) {
    const response = await axios.put(`${API_BASE_URL_DIGITAL}`, {
        ...newDigitalData
      }, {
        withCredentials: true
      });
  
    return response.data;
  }

  async function UpdateAnalog(newAnalogData) {
    const response = await axios.put(`${API_BASE_URL}`, {
        ...newAnalogData
      }, {
        withCredentials: true
      });
  
    return response.data;
  }

  async function GetAnalog() {
    const response = await axios.get(`${API_BASE_URL}`, {
        withCredentials: true
      });
  
    return response.data;
  }
  async function GetDigital() {
    const response = await axios.get(`${API_BASE_URL_DIGITAL}`, {
        withCredentials: true
      });
  
    return response.data;
  }



export {CreateAnalog,DeleteAnalog,CreateDigital,DeleteDigital,UpdateDigital,UpdateAnalog,GetAnalog,GetDigital}