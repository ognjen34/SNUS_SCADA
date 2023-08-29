import React, { useState } from 'react';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { AddUser } from '../services/userService';
function RegistrationForm({ onCloseDialog }) {
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();

        // Here you can perform your user registration logic
        // You might want to use an API call to send the registration data
        await AddUser(name, surname, email, password, [], [])
        // After registration, you can clear the form fields
        setName('');
        setSurname('');
        setEmail('');
        setPassword('');

        onCloseDialog();
    };

    const handleCloseDialog = (e) => {
        e.preventDefault();
        onCloseDialog();
    }

    return (
        <form onSubmit={handleSubmit}>
            <TextField
                label="Name"
                value={name}
                onChange={(e) => setName(e.target.value)}
                fullWidth
                margin="normal"
            />
            <TextField
                label="Surname"
                value={surname}
                onChange={(e) => setSurname(e.target.value)}
                fullWidth
                margin="normal"
            />
            <TextField
                label="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                fullWidth
                margin="normal"
            />
            <TextField
                label="Password"
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                fullWidth
                margin="normal"
            />
            <Button type="submit" variant="contained" color="primary">
                Register
            </Button>
            <Button onClick={handleCloseDialog} color="primary">
                        Cancel
            </Button>
        </form>
    );
}

export default RegistrationForm;
