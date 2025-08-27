import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './CSS/Sidebar.css';
import {logout} from '../services/authService';

const Sidebar = () => {
    const navigate = useNavigate();

    const handleLogout = () => {
        logout(navigate);
    };

    return (
        <div className="sidebar">
            <h2 className="sidebar-title">Account Information</h2>
            <ul className="sidebar-menu">
                <li>
                    <Link to="/cart">Cart</Link>
                </li>
                <li>
                    <Link to="/invoices">Invoices</Link>
                </li>
                <li>
                    <Link to="/account">User detail</Link>
                </li>
                <li>
                    <Link to="/review">User reviews</Link>
                </li>
                <li>
                    <button onClick={handleLogout} className="sidebar-logout-button">Logout</button>
                </li>
            </ul>
        </div>
    );
};

export default Sidebar;