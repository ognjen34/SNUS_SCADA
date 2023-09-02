import './Login.css';
import AdminHome from './AdminHome';
import ClientHome from './ClientHome';
import { GetAlarms } from '../services/reportService';
import React, { useEffect, useState } from 'react';
import { styled } from '@mui/material/styles';
import { Card, CardContent, Typography, IconButton, Box, Dialog, DialogContent } from '@mui/material';
import { Alarm, Add } from '@mui/icons-material';
import TableCell, { tableCellClasses } from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import { deleteDevice, getAllDevices } from '../services/deviceService';
import AddDevice from './AddDevice';

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: theme.palette.common.black,
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
  },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({
  '&:nth-of-type(odd)': {
    backgroundColor: theme.palette.action.hover,
  },
  '&:last-child td, &:last-child th': {
    border: 0,
  },
}));


function AlarmReport(props) {
  const [rows, setRows] = useState([]);
  const [startDate, setStartDate] = useState('');
  const [endDate, setEndDate] = useState('');

  const handleStartDateChange = (event) => {
    setStartDate(event.target.value);
  };

  const handleEndDateChange = (event) => {
    setEndDate(event.target.value);
  };

  const handleSubmit = () => {
    GetAlarms(startDate, endDate)
  .then((data) => {
    // Handle the response data here
    console.log('Alarms:', data);
    setRows(data)
  })
  .catch((error) => {
    // Handle errors here
  });
  };

  return (
    <div className="home-container">
      <div>
        <label>Start Date:</label>
        <input type="datetime-local" value={startDate} onChange={handleStartDateChange} />
      </div>
      <div>
        <label>End Date:</label>
        <input type="datetime-local" value={endDate} onChange={handleEndDateChange} />
      </div>
      <button onClick={handleSubmit}>Submit</button>
      <TableContainer sx={{ minWidth: 400 ,maxWidth:1000 , marginTop:10 }}component={Paper}>
      <Table sx={{ minWidth: 700 }} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>Id</StyledTableCell>
            <StyledTableCell align="right">Alarm Id</StyledTableCell>
            <StyledTableCell align="right">Alarm Type</StyledTableCell>
            <StyledTableCell align="right">Critical Value</StyledTableCell>
            <StyledTableCell align="right">Current Value</StyledTableCell>
    
          </TableRow>
        </TableHead>
        <TableBody>
          {rows.map((row) => (
            <StyledTableRow key={row.id}>
              <StyledTableCell component="th" scope="row">
                {row.id}
              </StyledTableCell>
              <StyledTableCell align="right">{row.alarmId}</StyledTableCell>
              <StyledTableCell align="right">{row.alarmType}</StyledTableCell>
              <StyledTableCell align="right">{row.criticalValue}</StyledTableCell>
              <StyledTableCell align="right">{row.value}</StyledTableCell>
            </StyledTableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
    
    </div>
  );
}

export default AlarmReport;
