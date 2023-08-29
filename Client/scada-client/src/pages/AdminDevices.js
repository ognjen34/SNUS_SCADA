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

function AdminDevices() {
    const [isDialogOpen, setIsDialogOpen] = useState(false);

    const handleDescriptionClick = () => {
        setIsDialogOpen(true);
      };
    
      const handleDialogClose = () => {
        setIsDialogOpen(false);
      };
      const handleChange = () => {
        SetChange(!change);
      };
    const [change, SetChange] = useState(true);
const handleRemoveClick = async (id) => {
        
        console.log(`Remove button clicked for row with ID: ${id}`);
        try {
            await deleteDevice(id);
            SetChange(!change)
            
        }
        catch(error)
        {
            
        }
        // Perform your remove logic here
      };
  const [rows, setRows] = useState([]);

  async function getDevices() {
    try {
      const data = await getAllDevices();
      setRows(data)
      console.log(data)
    } catch (error) {
      console.error(error);
    }
  }

  useEffect(() => {
    getDevices();
  }, [change]);

  return (
    <div>
        
    <TableContainer sx={{ minWidth: 400 ,maxWidth:1000 , marginTop:10 }}component={Paper}>
      <Table sx={{ minWidth: 700 }} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>Id</StyledTableCell>
            <StyledTableCell align="right">IOAdress</StyledTableCell>
            <StyledTableCell align="right">Device Type</StyledTableCell>
            <StyledTableCell align="right">Low Limit</StyledTableCell>
            <StyledTableCell align="right">High Limit</StyledTableCell>
            <StyledTableCell align="right">Simulation Type</StyledTableCell>
            <StyledTableCell align="right">Action</StyledTableCell>

          </TableRow>
        </TableHead>
        <TableBody>
          {rows.map((row) => (
            <StyledTableRow key={row.id}>
              <StyledTableCell component="th" scope="row">
                {row.id}
              </StyledTableCell>
              <StyledTableCell align="right">{row.ioAdress}</StyledTableCell>
              <StyledTableCell align="right">{row.deviceType}</StyledTableCell>
              <StyledTableCell align="right">{row.deviceConfig.lowLimit}</StyledTableCell>
              <StyledTableCell align="right">{row.deviceConfig.highLimit}</StyledTableCell>
              <StyledTableCell align="right">{row.deviceConfig.simulationType}</StyledTableCell>
              <button onClick={() => handleRemoveClick(row.id)}>Remove</button>
            </StyledTableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
    <IconButton color="primary" onClick={handleDescriptionClick}>
            <Add />
          </IconButton>
    <Dialog open={isDialogOpen} onClose={handleDialogClose}>
        <DialogContent>
            <AddDevice onClose={handleDialogClose} onChange={handleChange}/>
        </DialogContent>
    </Dialog>
    </div>
  );
}

export default AdminDevices;
