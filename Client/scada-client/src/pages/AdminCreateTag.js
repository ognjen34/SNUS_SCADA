import React, { useState } from 'react';
import {
  Dialog,
  DialogContent,
  TextField,
  Button,
  Typography,
  FormControl,
  RadioGroup,
  Radio,
  List,
  ListItem,
  FormControlLabel,
  Chip,
} from '@mui/material';
import { CreateAnalog,CreateDigital } from '../services/tagService';
const priorityOptions = ['LOW', 'MEDIUM', 'HIGH'];
const alarmTypeOptions = ['LOW', 'HIGH'];

function AdminCreateTag({ isOpen, onClose,onUpdateTags }) {
  const [updatedTagInfo, setUpdatedTagInfo] = useState({
    description: '',
    ioAddress: '',
    scan: false,
    scanTime: 0,
    units: 'L',
    type: 'Analog', // Added "type" field with default value "Analog"
  });

  const [newAlarm, setNewAlarm] = useState({
    id: '',
    type: 'LOW',
    priority: 'LOW',
    units: 'L',
    criticalValue: 0,
  });

  const [addedAlarms, setAddedAlarms] = useState([]);

  const [selectedAlarm, setSelectedAlarm] = useState(null);

  const handleFieldChange = (field, value) => {
    setUpdatedTagInfo(prevState => ({
      ...prevState,
      [field]: value,
    }));
  };

  const handleNewAlarmChange = (field, value) => {
    setNewAlarm(prevState => ({
      ...prevState,
      [field]: value,
    }));
  };

  const handleAddAlarm = () => {
    if (newAlarm.criticalValue !== undefined) {
      setAddedAlarms(prevState => [...prevState, newAlarm]);
      setNewAlarm({
        id: '',
        type: 'LOW',
        priority: 'LOW',
        units: 'L',
        criticalValue: 0,
      });
    }
  };

  const handleRemoveAlarm = index => {
    setAddedAlarms(prevState => {
      const alarms = [...prevState];
      alarms.splice(index, 1);
      return alarms;
    });
    setSelectedAlarm(null);
  };

  const handleAlarmClick = alarm => {
    setSelectedAlarm(alarm);
  };

  const handleUpdateClick = async () => {
    const updatedAlarms = addedAlarms.map(alarm => ({
      id: alarm.id,
      type: alarm.type === 'LOW' ? 0 : 1,
      priority: priorityOptions.indexOf(alarm.priority) + 1,
      units: alarm.units,
      criticalValue: parseFloat(alarm.criticalValue),
    }));
  
    const updatedTag = {
      description: updatedTagInfo.description,
      ioAddress: updatedTagInfo.ioAddress,
      scan: updatedTagInfo.scan,
      scanTime: updatedTagInfo.scanTime,
      unit: updatedTagInfo.units, // Check the field name and data type
      type: updatedTagInfo.type === 'Analog' ? 0 : 1,
      alarms: updatedAlarms,
    };
    
    if(updatedTag.type == 0){
        await CreateAnalog(updatedTag);
        onUpdateTags()

    }
    else{
        await CreateDigital(updatedTag);
        onUpdateTags()
    }
  };
  
  
  const [isAlarmDialogOpen, setAlarmDialogOpen] = useState(false);

  const handleOpenAlarmDialog = () => {
    setAlarmDialogOpen(true);
  };

  const handleCloseAlarmDialog = () => {
    setAlarmDialogOpen(false);
  };

  return (
    <Dialog open={isOpen} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogContent style={{ padding: '24px' }}>
        <Typography variant="h6" gutterBottom>
          Create New Tag
        </Typography>
        <form style={{ marginBottom: '24px' }}>
          <TextField
            label="Description"
            fullWidth
            value={updatedTagInfo.description}
            onChange={e => handleFieldChange('description', e.target.value)}
            style={{ marginBottom: '16px' }}
          />
          <TextField
            label="IO Address"
            fullWidth
            value={updatedTagInfo.ioAddress}
            onChange={e => handleFieldChange('ioAddress', e.target.value)}
            style={{ marginBottom: '16px' }}
          />
          <FormControl fullWidth style={{ marginBottom: '16px' }}>
            <RadioGroup
              value={updatedTagInfo.scan.toString()}
              onChange={e => handleFieldChange('scan', e.target.value === 'true')}
              row
            >
              <FormControlLabel value="true" control={<Radio />} label="Scan On" />
              <FormControlLabel value="false" control={<Radio />} label="Scan Off" />
            </RadioGroup>
          </FormControl>
          <TextField
            label="Scan Time"
            fullWidth
            type="number"
            value={updatedTagInfo.scanTime}
            onChange={e => handleFieldChange('scanTime', parseInt(e.target.value))}
            style={{ marginBottom: '16px' }}
          />
          <FormControl fullWidth style={{ marginBottom: '16px' }}>
            <RadioGroup
              value={updatedTagInfo.type}
              onChange={e => handleFieldChange('type', e.target.value)}
              row
            >
              <FormControlLabel value="Analog" control={<Radio />} label="Analog" />
              <FormControlLabel value="Digital" control={<Radio />} label="Digital" />
            </RadioGroup>
          </FormControl>
          {updatedTagInfo.type === 'Analog' && (
            <TextField
              label="Units"
              fullWidth
              value={updatedTagInfo.units}
              onChange={e => handleFieldChange('units', e.target.value)}
              style={{ marginBottom: '24px' }}
            />
          )}
          <Button
            variant="contained"
            color="primary"
            onClick={handleUpdateClick}
            style={{ marginTop: '16px' }}
          >
            Create
          </Button>
        </form>
        {updatedTagInfo.type === 'Analog' && (
          <Button variant="outlined" color="primary" onClick={handleOpenAlarmDialog}>
            Add Alarms
          </Button>
        )}
      </DialogContent>
      <AlarmDialog
        isOpen={isAlarmDialogOpen}
        onClose={handleCloseAlarmDialog}
        addedAlarms={addedAlarms}
        handleAddAlarm={handleAddAlarm}
        handleRemoveAlarm={handleRemoveAlarm}
        handleAlarmClick={handleAlarmClick}
        selectedAlarm={selectedAlarm}
        handleNewAlarmChange={handleNewAlarmChange}
        newAlarm={newAlarm}
      />
    </Dialog>
  );
}

function AlarmDialog({
  isOpen,
  onClose,
  addedAlarms,
  handleAddAlarm,
  handleRemoveAlarm,
  handleAlarmClick,
  selectedAlarm,
  handleNewAlarmChange,
  newAlarm,
}) {
  return (
    <Dialog open={isOpen} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogContent style={{ padding: '24px', minWidth: '500px' }}>
        <Typography variant="h6" gutterBottom>
          Add Alarms
        </Typography>
        <form style={{ marginBottom: '24px' }}>
          <FormControl fullWidth style={{ marginBottom: '16px' }}>
            <RadioGroup
              value={newAlarm.type}
              onChange={e => handleNewAlarmChange('type', e.target.value)}
              row
            >
              {alarmTypeOptions.map(type => (
                <FormControlLabel key={type} value={type} control={<Radio />} label={type} />
              ))}
            </RadioGroup>
          </FormControl>
          <FormControl fullWidth style={{ marginBottom: '16px' }}>
            <RadioGroup
              value={newAlarm.priority}
              onChange={e => handleNewAlarmChange('priority', e.target.value)}
              row
            >
              {priorityOptions.map(priority => (
                <FormControlLabel
                  key={priority}
                  value={priority}
                  control={<Radio />}
                  label={priority}
                />
              ))}
            </RadioGroup>
          </FormControl>
          <TextField
            label="Critical Value"
            type="number"
            fullWidth
            value={newAlarm.criticalValue}
            onChange={e => handleNewAlarmChange('criticalValue', e.target.value)}
            style={{ marginBottom: '24px' }}
          />
          <Button
            variant="outlined"
            color="primary"
            onClick={handleAddAlarm}
            style={{ marginTop: '16px' }}
          >
            Add Alarm
          </Button>
        </form>
        <div style={{ maxHeight: '200px', overflowY: 'auto', marginBottom: '24px' }}>
          <List>
            {addedAlarms.map((alarm, index) => (
              <ListItem key={index} button onClick={() => handleAlarmClick(alarm)}>
                <Chip
                  label={alarm.id === '' ? `Pending Alarm ${index + 1}` : alarm.id}
                  clickable
                />
              </ListItem>
            ))}
          </List>
        </div>
        <Dialog open={selectedAlarm !== null} onClose={() => handleAlarmClick(null)}>
          <DialogContent style={{ padding: '24px' }}>
            {selectedAlarm && (
              <>
                <Typography variant="h6" gutterBottom>
                  Alarm Information
                </Typography>
                <Typography>Alarm ID: {selectedAlarm.id}</Typography>
                <Typography>Type: {selectedAlarm.type}</Typography>
                <Typography>Priority: {selectedAlarm.priority}</Typography>
                <Typography>Critical Value: {selectedAlarm.criticalValue}</Typography>
                <Button
                  variant="outlined"
                  color="secondary"
                  onClick={() => handleRemoveAlarm(addedAlarms.indexOf(selectedAlarm))}
                  style={{ marginTop: '16px' }}
                >
                  Remove Alarm
                </Button>
              </>
            )}
          </DialogContent>
        </Dialog>
      </DialogContent>
    </Dialog>
  );
}

export default AdminCreateTag;
