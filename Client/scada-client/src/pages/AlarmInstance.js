import React from 'react';
import { Card, CardContent, Typography } from '@mui/material';

const AlarmInstance = ({ timestamp, message,value,criticalValue,units,alarmType}) => {
    const capitalizedAlarmType = alarmType.charAt(0).toUpperCase() + alarmType.slice(1).toLowerCase();

  return (
    <Card style={{ width: '99%', minHeight: '80px', marginBottom: '16px', borderRadius: '8px', border: '1px solid #ccc'}}>
      <CardContent>
        <Typography variant="subtitle2" color="textSecondary">
          {timestamp}
        </Typography>
        <Typography variant="body1">
          {message}
        </Typography>
        <Typography variant="body1">
          Alarm cutoff: {capitalizedAlarmType}er than {criticalValue}{units} 
        </Typography>
        <Typography variant="body1">
          Current Value: {value}{units}
        </Typography>
      </CardContent>
    </Card>
  );
};

export default AlarmInstance;
