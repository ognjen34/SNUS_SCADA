import './Login.css';
import AdminHome from './AdminHome';
import ClientHome from './ClientHome';
import { GetAnalogValuesFromTag } from '../services/reportService';
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

function AnalogValuesTagReport(props) {
  const [selectedId, setSelectedId] = useState('');
  const [rows, setRows] = useState([]);

  const handleIdChange = (event) => {
    setSelectedId(event.target.value);
  };

  const handleSubmit = () => {
    // Ensure you use the selectedId for your API call
    GetAnalogValuesFromTag(selectedId)
      .then((data) => {
        console.log('Analog Value:', data);
        // Handle the response data here
        setRows(data);
      })
      .catch((error) => {
        console.error('Error fetching analog value:', error);
      });
  };

  return (
    <div className="home-container">
      <div>
        <label>Enter ID:</label>
        <input type="text" value={selectedId} onChange={handleIdChange} />
      </div>
      <button onClick={handleSubmit}>Submit</button>
      <TableContainer sx={{ minWidth: 400, maxWidth: 1000, marginTop: 10 }} component={Paper}>
        <Table sx={{ minWidth: 700 }} aria-label="customized table">
          <TableHead>
            <TableRow>
              <StyledTableCell>Id</StyledTableCell>
              <StyledTableCell align="right">IOAdress</StyledTableCell>
              <StyledTableCell align="right">tag Id</StyledTableCell>
              <StyledTableCell align="right">Time</StyledTableCell>
              <StyledTableCell align="right">Value</StyledTableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {rows.map((row) => (
              <StyledTableRow key={row.id}>
                <StyledTableCell component="th" scope="row">
                  {row.id}
                </StyledTableCell>
                <StyledTableCell align="right">{row.ioAddress}</StyledTableCell>
                <StyledTableCell align="right">{row.tagId}</StyledTableCell>
                <StyledTableCell align="right">{row.timestamp}</StyledTableCell>
                <StyledTableCell align="right">{row.value}</StyledTableCell>
              </StyledTableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
}

export default AnalogValuesTagReport;
