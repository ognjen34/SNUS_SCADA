import React, { useState, useEffect } from 'react';
import { styled } from '@mui/material/styles';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell, { tableCellClasses } from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { GetClients } from '../services/userService';
import RegistrationForm from './RegistrationForm';
import Dialog from '@mui/material/Dialog';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle'; 
import Button from '@mui/material/Button'; 
import EditClientsAnalogInputs from './EditClientsAnalogInputs';
import EditClientsDigitalInputs from './EditClientsDigitalInputs';
import './AdminUsers.css';
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
  // hide last border
  '&:last-child td, &:last-child th': {
    border: 0,
  },
}));


function AdminUsers() {

    const [users, setUsers] = useState([]);
    const [rows, setRows] = useState([]);
    const [openDialog, setOpenDialog] = useState(false);
    const [openAnalogDialog, setOpenAnalogDialog] = useState(false);
    const [openDigitalDialog, setOpenDigitalDialog] = useState(false);
    const [selectedClient, setSelectedClient] = useState(users[0])

    const [change, SetChange] = useState(true);


    function handleAnalogButtonClick(index) {
      console.log(users[index]);
      setSelectedClient(users[index])
      setOpenAnalogDialog(true);
    }

    function handleDigitalButtonClick(index) {
      console.log(users[index]);
      setSelectedClient(users[index])
      setOpenDigitalDialog(true)
    }

    const handleOpenDialog = () => {
        setOpenDialog(true);
    };

    const handleCloseDialog = () => {
        SetChange(!change)
        setOpenDialog(false);
        setOpenAnalogDialog(false)
        setOpenDigitalDialog(false)
    };

    useEffect(() => {
        async function fetchClients() {
          try {
            const response = await GetClients();
            setUsers(response)
            let clients = []
            response.forEach((client, index) => {
                let fullname = client.name + " " + client.surname;
                clients.push({ "fullname": fullname, "index": index });
            });
            setRows(clients)
            console.log("rows:")
            console.log(rows)
          } catch (error) {
            console.error("Error fetching clients:", error);
          }
        }
    
        fetchClients();
      }, [change]); 
    return (
        <div className='center-div'>
            
            <Dialog open={openDialog} onClose={handleCloseDialog}>
                <DialogTitle>Register User</DialogTitle>
                <DialogContent>
                    {/* Pass the handleCloseDialog function to the RegistrationForm */}
                    <RegistrationForm onCloseDialog={handleCloseDialog} />
                </DialogContent>
            </Dialog>

            <Dialog open={openAnalogDialog} onClose={handleCloseDialog} >
                <DialogTitle>Edit Analog inputs</DialogTitle>
                <DialogContent>
                    {/* Pass the handleCloseDialog function to the RegistrationForm */}
                    <EditClientsAnalogInputs onCloseDialog={handleCloseDialog} selectedclient = {selectedClient}></EditClientsAnalogInputs>
                    
                </DialogContent>
            </Dialog>

            <Dialog open={openDigitalDialog} onClose={handleCloseDialog}>
                <DialogTitle>Edit Digital Inputs</DialogTitle>
                <DialogContent>
                    {/* Pass the handleCloseDialog function to the RegistrationForm */}
                    <EditClientsDigitalInputs onCloseDialog={handleCloseDialog} selectedclient = {selectedClient}></EditClientsDigitalInputs>
                </DialogContent>
            </Dialog>


            <TableContainer component={Paper} className="table-container">
            <Table aria-label="customized table">
            <TableHead>
                <TableRow>
                <StyledTableCell>Users</StyledTableCell>
                <StyledTableCell align="right">Analog Inputs</StyledTableCell>
                <StyledTableCell align="right">Digital Inputs</StyledTableCell>

                </TableRow>
            </TableHead>
            <TableBody>
                {rows.map((row) => (
                <StyledTableRow key={row.fullname}>
                    <StyledTableCell component="th" scope="row">
                    {row.fullname}
                    </StyledTableCell>
                    <StyledTableCell align="right"><button onClick={() => handleAnalogButtonClick(row.index)}>Edit</button></StyledTableCell>
                    <StyledTableCell align="right"><button onClick={() => handleDigitalButtonClick(row.index)}>Edit</button></StyledTableCell>
                </StyledTableRow>
                ))}
            </TableBody>
            </Table>
            </TableContainer >
            <button onClick={handleOpenDialog} className='button-margin'>Add User</button>
        </div>
        
    );
}

export default AdminUsers;
