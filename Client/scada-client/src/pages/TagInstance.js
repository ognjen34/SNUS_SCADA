import React, { useState } from 'react';
import { Card, CardContent, Typography, IconButton, Box, Dialog, DialogContent } from '@mui/material';
import { Alarm, Description, Delete, Update } from '@mui/icons-material';
import {DeleteAnalog, DeleteDigital} from "../services/tagService";
import AdminUpdateTag from './AdminUpdateTag';

const TagInstance = ({ id, description, isAdmin, ioAddress, scan, scanTime, units, alarms, value,tagType,onDeleteTag,onUpdateTags  }) => {
  if (alarms === undefined) {
    alarms = [];
  }
  if (units === undefined) {
    units = "W";
  }
  const [isAlarmActive, setIsAlarmActive] = useState(false);
  const [isDescriptionDialogOpen, setIsDescriptionDialogOpen] = useState(false);

  const handleAlarmClick = () => {
    setIsAlarmActive(!isAlarmActive);
    // Implement alarm click logic here
  };

  const handleDescriptionClick = () => {
    setIsDescriptionDialogOpen(true);
  };

  const handleDescriptionDialogClose = () => {
    setIsDescriptionDialogOpen(false);
  };
  const [isUpdateDialogOpen, setIsUpdateDialogOpen] = useState(false);

  const handleUpdateClick = () => {
    setIsUpdateDialogOpen(true);
  };

  const handleDeleteClick = async () =>{
    if(tagType == "ANALOG"){
      await DeleteAnalog(id);
    }
    else{
      await DeleteDigital(id);
    }
    onDeleteTag(id, tagType);
  }
  const handleUpdateDialogClose = () => {
    setIsUpdateDialogOpen(false);
  };
  return (
    <Card style={{ minWidth: '430px', maxWidth: '700px', minHeight: '300px', maxHeight: '400px', marginBottom: '16px', marginTop: '30px', marginRight: '16px', marginLeft: '73px', boxShadow: '0px 0px 5px rgba(0, 0, 0, 0.3)' }}>
      <CardContent style={{ display: 'flex', flexDirection: 'column', justifyContent: 'space-between', height: '100%' }}>
        <div>
          <Typography variant="h6" component="div" style={{ marginBottom: '8px' }}>
            {id}
          </Typography>
        </div>
        <div style={{ display: 'flex', justifyContent: 'center', marginBottom: '16px' }}>
          <Box display="flex" alignItems="center" justifyContent="center" minHeight={100} minWidth="30%" maxWidth="70%">
            {/* Display value here */}
            <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', border: '2px solid black', padding: '8px', borderRadius: '8px', width: '100%' }}>
              <Typography variant="h4" component="div">
                {Math.floor(value)}
              </Typography>
            </div>
          </Box>
        </div>
        {/* Display additional info */}
        <div style={{ display: 'flex', flexDirection: 'column', marginBottom: '16px' }}>
          <Typography variant="body1">
            IO Address: Scanning from {ioAddress}
          </Typography>
          <Typography variant="body1">
            Scan: {scan ? 'On' : 'Off'}
          </Typography>
          <Typography variant="body1">
            Scan Time: {scanTime} seconds
          </Typography>
          <Typography variant="body1">
            Units: {units}
          </Typography>
        </div>
        <div style={{ display: 'flex', justifyContent: 'flex-end', alignItems: 'center', marginBottom: '16px' }}>
          <IconButton color="primary" onClick={handleDescriptionClick}>
            <Description />
          </IconButton>
          {isAdmin && (
            <>
              <IconButton color="primary" onClick={handleUpdateClick}>
                <Update />
              </IconButton>
              <IconButton color="primary" onClick={handleDeleteClick}>
                <Delete />
              </IconButton>
            </>
          )}
        </div>
        <Dialog open={isDescriptionDialogOpen} onClose={handleDescriptionDialogClose}>
            <DialogContent>
              <Typography>{description}</Typography>
            </DialogContent>
          </Dialog>
        <Dialog
          open={isUpdateDialogOpen}
          onClose={handleUpdateDialogClose}
          maxWidth="sm"
          fullWidth
        >
          <AdminUpdateTag tagInfo={{  id, description, isAdmin, ioAddress, scan, scanTime, units, alarms, value,tagType }} onUpdateTags={onUpdateTags} isOpen={isUpdateDialogOpen} onClose={handleUpdateDialogClose} />
        </Dialog>
      </CardContent>
    </Card>
  );
};

export default TagInstance;
