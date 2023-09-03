import axios from 'axios';

const API_BASE_URL = 'http://localhost:5045/api/User';


async function GetClients(){
    const response = await axios.get(`${API_BASE_URL}/clients`, {
        withCredentials: true
      });
      console.log("Klijenti")
      console.log(response.data)
      return response.data;
}

async function AddUser(name, surname, email,password, analogInputsIds, digitalInputsIds) {
    const response = await axios.post(`${API_BASE_URL}/register`, {
        name,
      surname,
      email,
      password,
      analogInputsIds,
      digitalInputsIds
    }, {
      withCredentials: true
    });
  
    return response;
  }

  async function UpdateUser( name, surname, email,password,analogInputsIds, digitalInputsIds) {
    const response = await axios.put(`${API_BASE_URL}/update`, {
      'Name': name,
    'Surname': surname,
    'Email': email,
    'Password': password,
    'AnalogInputsIds': analogInputsIds,
    'DigitalInputsIds': digitalInputsIds
  }, {
        withCredentials: true
      });
  
    return response.data;
  }

export {GetClients, AddUser, UpdateUser}