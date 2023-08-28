import React, { useState } from 'react';
import { Card, CardContent, Typography, IconButton, Box } from '@mui/material';
import { Alarm, Description } from '@mui/icons-material';

const TagInstance = ({ id, description, ioAddress,value }) => {
    const [isAlarmActive, setIsAlarmActive] = useState(false);
    const [isDescriptionActive, setIsDescriptionActive] = useState(false);
  
    const handleAlarmClick = () => {
      setIsAlarmActive(!isAlarmActive);
      // Implement alarm click logic here
    };
  
    const handleDescriptionClick = () => {
      setIsDescriptionActive(!isDescriptionActive);
      // Implement description click logic here
    };
  
    return (
      <Card>
        <CardContent>
          <Typography variant="h6" component="div">
            {id}
          </Typography>
          <Box display="flex" alignItems="center" justifyContent="center" minHeight={100}>
            <Typography variant="h4" component="div">
              {value}
            </Typography>
          </Box>
          <Box display="flex" justifyContent="space-between">
            <IconButton color={isAlarmActive ? 'secondary' : 'primary'} onClick={handleAlarmClick}>
              <Alarm />
            </IconButton>
            <IconButton color={isDescriptionActive ? 'secondary' : 'primary'} onClick={handleDescriptionClick}>
              <Description />
            </IconButton>
          </Box>
        </CardContent>
      </Card>
    );
  };

export default TagInstance;