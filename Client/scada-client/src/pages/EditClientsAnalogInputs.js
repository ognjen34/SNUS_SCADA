import React, { useState, useEffect } from 'react';
import Button from '@mui/material/Button';
import Chip from '@mui/material/Chip';
import { GetAllAnalog } from '../services/tagService';
import { UpdateUser } from '../services/userService';

function EditClientsAnalogInputs({ onCloseDialog, selectedclient }) {
    const [analogInputs, setAnalogInputs] = useState([]);
    const [usersAnalogInput, setUsersAnalogInput] = useState([]);
    const [selectedAnalogInputId, setSelectedAnalogInputId] = useState('');
    const [user, setUser] = useState({})

    const handleCloseDialog = (e) => {
        e.preventDefault();
        onCloseDialog();
    };

    const handleSaveButton = async (e) => {
        e.preventDefault();

        selectedclient['analogInputs'] = usersAnalogInput
        console.log("Novi brat:")

        console.log(selectedclient)

        try{
            const updatedUser = await UpdateUser(selectedclient['name'], selectedclient['surname'],selectedclient['email'],  selectedclient['password'], selectedclient['analogInputs'], selectedclient['digitalInputs'].map(obj => obj.id))
            console.log(updatedUser)
        }
        catch(error){
            console.log(error)
        }
        onCloseDialog()
    }

    const handleAnalogInputSelect = (e) => {
        setSelectedAnalogInputId(e.target.value);
    };

    const handleAnalogInputAdd = () => {
        if (selectedAnalogInputId) {
            setUsersAnalogInput((prevInputs) => [...prevInputs, selectedAnalogInputId]);
            setSelectedAnalogInputId('');
        }
    };

    useEffect(() => {
        async function getAnalogInputs() {
            try {
                const response = await GetAllAnalog();
                setAnalogInputs(response);
            } catch (error) {
                console.log(error);
            }
        }
        if (selectedclient) {
            console.log(selectedclient)
            setUsersAnalogInput(selectedclient['analogInputs'].map(obj => obj.id));
            console.log("mapapappapap::::")
            console.log(selectedclient['analogInputs'].map(obj => obj.id))
            setUser(selectedclient)
            
        }

        getAnalogInputs();
    }, [selectedclient]);

    return (
        <div style={{ display: 'flex', flexDirection: 'column'}}>
            <div>
                <select value={selectedAnalogInputId} onChange={handleAnalogInputSelect}>
                    <option value="" disabled>
                        Select an Analog Input
                    </option>
                    {analogInputs.map((input) => (
                        <option key={input.id} value={input.id}>
                            {input.id}
                        </option>
                    ))}
                </select>
                <Button variant="contained" onClick={handleAnalogInputAdd} style={{ marginLeft: '8px' }}>
                    Add
                </Button>
            </div>
            {usersAnalogInput.length > 0 && (
                <div style={{ display: 'flex', flexWrap: 'wrap', marginTop: '16px' }}>
                    {usersAnalogInput.map((inputId, index) => (
                        <Chip
                            key={index}
                            label={`${inputId}`}
                            onDelete={() => {
                                setUsersAnalogInput((prevInputs) =>
                                    prevInputs.filter((_, i) => i !== index)
                                );
                            }}
                            style={{ marginRight: '8px', marginTop: '4px' }}
                        />
                    ))}
                </div>
            )}
            <div>
                <button onClick={handleSaveButton} variant="contained" style={{ marginLeft: '8px' }}>
                    Save
                </button>
                <Button onClick={handleCloseDialog}>Cancel</Button>
            </div>
        </div>
    );
}

export default EditClientsAnalogInputs;
