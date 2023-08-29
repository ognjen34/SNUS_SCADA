import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Menu from '@mui/material/Menu';
import MenuIcon from '@mui/icons-material/Menu';
import Container from '@mui/material/Container';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import Tooltip from '@mui/material/Tooltip';
import MenuItem from '@mui/material/MenuItem';
import AdbIcon from '@mui/icons-material/Adb';
import { Outlet, Link, useNavigate } from 'react-router-dom';
import { LogOut } from '../services/authService';

const pages = ['Tags', 'Users', 'Devices'];
const settings = ['Logout'];

function AdminHome(props) {
    console.log(props.data)
    const [anchorElNav, setAnchorElNav] = React.useState(null);
    const [anchorElUser, setAnchorElUser] = React.useState(null);

    const navigate = useNavigate();

    const handleOpenNavMenu = (event) => {
        setAnchorElNav(event.currentTarget);
    };

    const handleOpenUserMenu = (event) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElNav(null);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const handleLogOut = async () => 
    {
        await LogOut();
        window.location.reload();
    }

    return (
        <div>
            <AppBar position="static">
                <Container maxWidth="l">
                    <Toolbar disableGutters>
                    <div>
                        <Link to="tags">
                            <button className='btn menu-btn'>Tags</button>
                        </Link>
                        <Link to="users">
                            <button className='btn menu-btn'>Users</button>
                        </Link>
                        <Link to="devices">
                            <button className='btn menu-btn'>Devices</button>
                        </Link>
                        
                        <button onClick = {handleLogOut} className='btn menu-btn'>Logout</button>
                        

                    </div>
                    </Toolbar>
                </Container>
            </AppBar>
            <div className="main-content" style={{ display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <Outlet data={props.data} />
            </div>
        </div>
    );
}

export default AdminHome;
