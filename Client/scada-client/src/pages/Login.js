import React, { useState } from 'react';
import './Login.css';
import { SignIn } from '../services/authService';
import { useNavigate } from 'react-router-dom';

    function Login() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();
    const handleLogin = async (e) => {
        e.preventDefault();
        // Replace this with your actual login logic
        const userData = await SignIn(username, password);
        
        if (userData.role === 0) {
          window.location.reload(); // This can be improved, but for simplicity, I'll keep it as is
          navigate('/home/tags'); // Redirect to the desired route if the user is an admin
        } else {
          // Handle non-admin user (you can show an error message or redirect them to a different route)
          window.location.reload()
        }
      };

    return (
        <div className='login-box'>
        <div className="login-container">
        <form className="login-form" onSubmit={handleLogin}>
            <h1>Login</h1>
            <input
            type="text"
            placeholder="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            />
            <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            />
            <button type="submit">Login</button>
        </form>
        </div>
        </div>
    );
    }

export default Login;
