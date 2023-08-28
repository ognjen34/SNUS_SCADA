import React, { useState } from 'react';
import { Card, CardContent, Typography, IconButton, Box, Dialog, DialogContent } from '@mui/material';
import { Alarm, Description } from '@mui/icons-material';

const TagInstance = ({ id, description, ioAddress, value }) => {
  const [isAlarmActive, setIsAlarmActive] = useState(false);
  const [isDescriptionActive, setIsDescriptionActive] = useState(false);
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  const handleAlarmClick = () => {
    setIsAlarmActive(!isAlarmActive);
    // Implement alarm click logic here
  };

  const handleDescriptionClick = () => {
    setIsDialogOpen(true);
  };

  const handleDialogClose = () => {
    setIsDialogOpen(false);
  };

  return (
    <Card style={{ minWidth: '300px', maxWidth: '500px', minHeight: '300px', maxHeight: '400px', marginBottom: '16px',marginTop:'30px', marginRight: '16px',marginLeft:'50px', boxShadow: '0px 0px 5px rgba(0, 0, 0, 0.3)' }}>
      <CardContent style={{ display: 'flex', flexDirection: 'column', justifyContent: 'space-between', height: '100%' }}>
        <div>
          <Typography variant="h6" component="div" style={{ marginBottom: '8px' }}>
            {id}
          </Typography>
        </div>
        <div style={{ display: 'flex', justifyContent: 'center', marginBottom: '16px' }}>
        <Box display="flex" alignItems="center" justifyContent="center" minHeight={100} minWidth="30%" maxWidth="70%">
            <div style={{ display: 'flex', alignItems: 'center',justifyContent: 'center', border: '2px solid black', padding: '8px', borderRadius: '8px', width: '100%' }}>
              <Typography variant="h4" component="div">
                {Math.floor(value)}
              </Typography>
            </div>
          </Box>
          </div>
        <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '16px' }}>
          <IconButton color={isAlarmActive ? 'secondary' : 'primary'} onClick={handleAlarmClick}>
            <Alarm />
          </IconButton>
          <IconButton color="primary" onClick={handleDescriptionClick}>
            <Description />
          </IconButton>
          <Dialog open={isDialogOpen} onClose={handleDialogClose}>
            <DialogContent>
              <Typography>{description}</Typography>
            </DialogContent>
          </Dialog>
        </div>
      </CardContent>
    </Card>
  );
};

export default TagInstance;
