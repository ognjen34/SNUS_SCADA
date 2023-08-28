import React, { useState } from 'react';
import './Login.css';
import AdminHome from './AdminHome';
import ClientHome from './ClientHome';

function Home(props) {
    const { data } = props;
    console.log(data)
    {console.log("xd")}
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = async (e) => {
        e.preventDefault();
        

    };

    return (
        <div className="home-container">
            {data.role === 0 ? <AdminHome data={data}/> : <ClientHome data={data}/>}
        </div>
    );
}

export default Home;
