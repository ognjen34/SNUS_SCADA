import axios from 'axios';

const API_BASE_URL = 'https://localhost:7045/api';



async function GetAlarms(startDate, endDate) {
    try {
      const response = await axios.get(`${API_BASE_URL}/Alarm/data`, {
        params: {
          startDate: startDate, 
          endDate: endDate,
        },
      });
      return response.data;
    } catch (error) {
      console.error('Error fetching alarms:', error);
      throw error;
    }
  }

  async function GetAnalogDate(startDate, endDate) {
    try {
      const response = await axios.get(`${API_BASE_URL}/AnalogInput/data`, {
        params: {
          startDate: startDate, 
          endDate: endDate,
        },
      });
      return response.data;
    } catch (error) {
      console.error('Error fetching alarms:', error);
      throw error;
    }
  }
  async function GetDigitalDate(startDate, endDate) {
    try {
      const response = await axios.get(`${API_BASE_URL}/DigitalInput/data`, {
        params: {
          startDate: startDate, 
          endDate: endDate,
        },
      });
      return response.data;
    } catch (error) {
      console.error('Error fetching alarms:', error);
      throw error;
    }
  }

  async function GetAnalogLastValues() {
    try {
      const response = await axios.get(`${API_BASE_URL}/AnalogInput/digitalreads`, {
      });
      return response.data;
    } catch (error) {
      console.error('Error fetching alarms:', error);
      throw error;
    }
  }
  async function GetDigitalLastValues() {
    try {
      const response = await axios.get(`${API_BASE_URL}/DigitalInput/digitalreads`, {
      });
      return response.data;
    } catch (error) {
      console.error('Error fetching alarms:', error);
      throw error;
    }
  }
  async function GetDigitalValuesFromTag(id) {
    try {
      const response = await axios.get(`${API_BASE_URL}/DigitalInput/`+id, {
      });
      return response.data;
    } catch (error) {
      console.error('Error fetching alarms:', error);
      throw error;
    }
  }
  async function GetAnalogValuesFromTag(id) {
    try {
      const response = await axios.get(`${API_BASE_URL}/AnalogInput/`+id, {
      });
      return response.data;
    } catch (error) {
      console.error('Error fetching alarms:', error);
      throw error;
    }
  }
export {GetAlarms,GetDigitalDate,GetAnalogDate,GetAnalogLastValues,GetDigitalLastValues,GetAnalogValuesFromTag,GetDigitalValuesFromTag}