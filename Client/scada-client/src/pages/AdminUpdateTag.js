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
import { UpdateAnalog, UpdateDigital } from '../services/tagService';

const priorityOptions = ['LOW', 'MEDIUM', 'HIGH'];
const alarmTypeOptions = ['LOW', 'HIGH'];

function AdminUpdateTag({ tagInfo, isOpen, onClose,onUpdateTags }) {
    console.log(tagInfo);
    console.log(tagInfo);
  if (tagInfo.alarms == undefined) {
    tagInfo.alarms = [];
  }
  const [updatedTagInfo, setUpdatedTagInfo] = useState(tagInfo);
  const [newAlarm, setNewAlarm] = useState({
    id: '',
    type: 'LOW',
    priority: 'LOW',
    units: 'L',
    criticalValue: 0,
  });
  const [addedAlarms, setAddedAlarms] = useState(tagInfo.alarms);
  const [selectedAlarm, setSelectedAlarm] = useState(null);
  console.log(addedAlarms);
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
    console.log(addedAlarms);
    const updatedAlarms = addedAlarms.map(alarm => {
        let alarmType = alarm.type;
        if (typeof alarmType === 'string') {
          alarmType = alarmType === 'LOW' ? 0 : 1;
        }
      
        let alarmPriority = alarm.priority;
        if (typeof alarmPriority === 'string') {
          alarmPriority = priorityOptions.indexOf(alarmPriority) + 1;
        }
      
        return {
          id: alarm.id,
          type: alarmType,
          priority: alarmPriority,
          units: alarm.units,
          criticalValue: parseFloat(alarm.criticalValue),
        };
      });
      const updatedTag = {
        id: updatedTagInfo.id,
        description: updatedTagInfo.description,
        ioAddress: updatedTagInfo.ioAddress,
        scan: updatedTagInfo.scan,
        scanTime: updatedTagInfo.scanTime,
        unit: updatedTagInfo.units, // Check the field name and data type
        type: updatedTagInfo.type === 'Analog' ? 0 : 1,
        alarms: updatedAlarms,
      };
      
      if(updatedTagInfo.tagType === 'ANALOG'){
        await UpdateAnalog(updatedTag);
        onUpdateTags();
      }
      else{
          await UpdateDigital(updatedTag);
          onUpdateTags();
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
          Update Tag Information
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
          {tagInfo.tagType === 'DIGITAL' ? (
            <></> 
          ) : (
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
            Update
          </Button>
        </form>
        {updatedTagInfo.tagType === 'ANALOG' && (
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

export default AdminUpdateTag;
