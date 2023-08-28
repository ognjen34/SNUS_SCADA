import React, { useState } from 'react';
import './Login.css';
import { SignIn } from '../services/authService';
function Login() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async (e) => {
    e.preventDefault();
    // Replace this with your actual login logic
    const data = await SignIn(username, password,);

  };

  return (
    <div className="login-box">
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
