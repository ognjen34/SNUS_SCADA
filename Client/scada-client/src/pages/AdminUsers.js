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

    function handleButtonClick(index) {
        console.log(users[index]);
    }

    const handleOpenDialog = () => {
        setOpenDialog(true);
    };

    const handleCloseDialog = () => {
        setOpenDialog(false);
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
      }, []); 
    return (
        <div>
            
            <Dialog open={openDialog} onClose={handleCloseDialog}>
                <DialogTitle>Register User</DialogTitle>
                <DialogContent>
                    {/* Pass the handleCloseDialog function to the RegistrationForm */}
                    <RegistrationForm onCloseDialog={handleCloseDialog} />
                </DialogContent>
            </Dialog>
            <TableContainer component={Paper}>
            <Table sx={{ Width: 700 }} aria-label="customized table">
            <TableHead>
                <TableRow>
                <StyledTableCell>Users</StyledTableCell>
                <StyledTableCell align="right">Manage Tags</StyledTableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {rows.map((row) => (
                <StyledTableRow key={row.fullname}>
                    <StyledTableCell component="th" scope="row">
                    {row.fullname}
                    </StyledTableCell>
                    <StyledTableCell align="right"><button onClick={() => handleButtonClick(row.index)}>Add Tags</button></StyledTableCell>

                </StyledTableRow>
                ))}
            </TableBody>
            </Table>
            </TableContainer>
            <button onClick={handleOpenDialog}>Add User</button>
        </div>
        
    );
}

export default AdminUsers;
