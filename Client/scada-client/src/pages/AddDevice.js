import './Login.css';
import React, { useState } from 'react';
import TextField from '@mui/material/TextField';
import MenuItem from '@mui/material/MenuItem';
import Select from '@mui/material/Select';
import InputLabel from '@mui/material/InputLabel';
import FormControl from '@mui/material/FormControl';
import Paper from '@mui/material/Paper';
import Button from '@mui/material/Button';
import { addDevice } from '../services/deviceService';


function AddDevice(props) {
    const [ioAdress, setIoAdress] = useState('');
    const [deviceType, setDeviceType] = useState('');
    const [lowLimit, setLowLimit] = useState('');
    const [highLimit, setHighLimit] = useState('');
    const [simulationType, setSimulationType] = useState('');
  
    const handleFormSubmit = async (e) => {
        e.preventDefault();
      
        // Parse the values to numbers
        const parsedDeviceType = parseInt(deviceType, 10);
        const parsedLowLimit = parseFloat(lowLimit);
        const parsedHighLimit = parseFloat(highLimit);
        const parsedSimulationType = parseInt(simulationType, 10);
      
        const newDevice = {
          ioAdress: ioAdress,
          deviceType: parsedDeviceType,
          deviceConfig: {
            lowLimit: parsedLowLimit,
            highLimit: parsedHighLimit,
            simulationType: parsedSimulationType,
          },
        };
      
        // Now you can do something with the newDevice object, like sending it to an API
        await addDevice(newDevice)
        console.log(newDevice);
        props.onClose();
        props.onChange();
      };

    return (
        <Paper elevation={3} className="form-container">
          <form onSubmit={handleFormSubmit}>
            <TextField
              label="IO Address"
              value={ioAdress}
              onChange={(e) => setIoAdress(e.target.value)}
              fullWidth
              required
            />
            <FormControl fullWidth>
              <InputLabel>Device Type</InputLabel>
              <Select
                value={deviceType}
                onChange={(e) => setDeviceType(e.target.value)}
                required
              >
                <MenuItem value="0">SIMULATION</MenuItem>
                <MenuItem value="1">RTU</MenuItem>
                {/* Add more options */}
              </Select>
            </FormControl>
            <TextField
              type="number"
              label="Low Limit"
              value={lowLimit}
              onChange={(e) => setLowLimit(e.target.value)}
              fullWidth
              required
            />
            <TextField
              type="number"
              label="High Limit"
              value={highLimit}
              onChange={(e) => setHighLimit(e.target.value)}
              fullWidth
              required
            />
            <FormControl fullWidth>
              <InputLabel>Simulation Type</InputLabel>
              <Select
                value={simulationType}
                onChange={(e) => setSimulationType(e.target.value)}
                required
              >
                <MenuItem value="0">SIN</MenuItem>
                <MenuItem value="1">COS</MenuItem>
                <MenuItem value="2">RAMP</MenuItem>
                <MenuItem value="3">RTU</MenuItem>

                {/* Add more options */}
              </Select>
            </FormControl>
            <Button type="submit" variant="contained" color="primary">
              Submit
            </Button>
          </form>
        </Paper>
      );
}

export default AddDevice;
