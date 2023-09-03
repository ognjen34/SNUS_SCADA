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
  const [ioAddress, setIoAddress] = useState('');
  const [deviceType, setDeviceType] = useState('');
  const [lowLimit, setLowLimit] = useState('');
  const [highLimit, setHighLimit] = useState('');
  const [simulationType, setSimulationType] = useState('');

  const handleFormSubmit = async (e) => {
    e.preventDefault();

    // Parse the values to numbers
    const parsedDeviceType = parseInt(deviceType, 10);

    // Set lowLimit and highLimit based on the device type
    let parsedLowLimit = parseFloat(lowLimit);
    let parsedHighLimit = parseFloat(highLimit);

    if (parsedDeviceType === 0) {
      // For "SIMULATION" type, set lowLimit and highLimit to -1
      parsedLowLimit = -1;
      parsedHighLimit = -1;
    }

    // Set simulationType based on the device type
    let parsedSimulationType = parseInt(simulationType, 10);
    if (parsedDeviceType === 1) {
      // For "RTU" type, set simulationType to 3 ("RTU")
      parsedSimulationType = 3;
    }

    const newDevice = {
      ioAdress: ioAddress,
      deviceType: parsedDeviceType,
      deviceConfig: {
        lowLimit: parsedLowLimit,
        highLimit: parsedHighLimit,
        simulationType: parsedSimulationType,
      },
    };

    // Now you can do something with the newDevice object, like sending it to an API
    await addDevice(newDevice);
    console.log(newDevice);
    props.onClose();
    props.onChange();
  };

  return (
    <div>
      <form onSubmit={handleFormSubmit}>
        <TextField
          label="IO Address"
          value={ioAddress}
          onChange={(e) => setIoAddress(e.target.value)}
          fullWidth
          required
          margin="normal"
        />
        <FormControl fullWidth margin="normal">
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
        {deviceType === "1" && (
          <>
            <TextField
              type="number"
              label="Low Limit"
              value={lowLimit}
              onChange={(e) => setLowLimit(e.target.value)}
              fullWidth
              required
              margin="normal"
            />
            <TextField
              type="number"
              label="High Limit"
              value={highLimit}
              onChange={(e) => setHighLimit(e.target.value)}
              fullWidth
              required
              margin="normal"
            />
          </>
        )}
        {deviceType === "0" && (
          <FormControl fullWidth margin="normal">
            <InputLabel>Simulation Type</InputLabel>
            <Select
              value={simulationType}
              onChange={(e) => setSimulationType(e.target.value)}
              required
            >
              <MenuItem value="0">SIN</MenuItem>
              <MenuItem value="1">COS</MenuItem>
              <MenuItem value="2">RAMP</MenuItem>
            </Select>
          </FormControl>
        )}
        <Button type="submit" variant="contained" color="primary">
          Submit
        </Button>
      </form>
    </div>
  );
}

export default AddDevice;
