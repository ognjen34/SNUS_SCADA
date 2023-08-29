import axios from 'axios';

const API_BASE_URL = 'http://localhost:5045/api/User';


async function SignIn(username, password,recaptcha) {
    const response = await axios.post(`${API_BASE_URL}/login`, {
      username,
      password,
    }, {
      withCredentials: true
    });
  
    return response.data;
  }


async function checkCookieValidity() {
    const response = await axios.get(`${API_BASE_URL}/authorized`, {
      withCredentials: true
    });
    console.log(response)
    return response.data;
  }

async function LogOut() {
    const response = await axios.get(`${API_BASE_URL}/logout`, {
      withCredentials: true
    });
    console.log(response)
    return response.data;
  }

export {checkCookieValidity,SignIn,LogOut}