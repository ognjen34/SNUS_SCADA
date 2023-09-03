import {Routes, Route, Navigate, useNavigate} from 'react-router-dom';
import Login from './pages/Login';
import Home from './pages/Home';
import { useState, useEffect } from 'react';
import {checkCookieValidity} from './services/authService'
import AdminTags from './pages/AdminTags';
import AdminUsers from './pages/AdminUsers';
import AdminDevices from './pages/AdminDevices';
import Reports from './pages/Reports';

function App() {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    let navigate = useNavigate();

    const [data, setData] = useState('')
    async function checkAuthentication() {
      try {
        const data = await checkCookieValidity();
        setData(data)
        setIsAuthenticated(true)
      } catch (error) {
        console.error(error);
        setIsAuthenticated(false);

      }
    }
    useEffect(() => {   
      console.log("XD")
      checkAuthentication();
  }, []);

    return (
        <Routes>
          <Route
                path="/"
                element={
                    isAuthenticated ? <Navigate to="/home" /> : <Navigate to="/login" />
                }
            />
            <Route
                path="/login"
                element={
                    isAuthenticated ? (
                        <Navigate to="/home" />
                    ) : (
                        <Login />
                    )
                    
                }
            />
            <Route
                path="/home"
                element={
                    isAuthenticated ? (
                        <Home data={data} />
                    ) : (
                        <Navigate to="/login" />
                    )
                    
                }>
                <Route path="tags" element={<AdminTags  data={data}/>}/>
                <Route path="users" element={<AdminUsers />}/>
                <Route path="reports" element={<Reports />}/>
                <Route path="devices" element = {<AdminDevices/>} />     

            </Route>
            
        </Routes>
    );
}

export default App;