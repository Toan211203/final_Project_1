import React, { useEffect, useState } from 'react';
import { getUserIdFromToken, getUserRoleFromToken, getAuthHeaders } from '../../services/userAuth';
import { logout } from '../../services/authService';
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import './CSS/dashboard.css';

const API_BASE_URL = 'http://localhost:5231';

const DashboardPage = () => {
    const [userInfo, setUserInfo] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchUserInfo = async () => {
            const userId = parseInt(getUserIdFromToken(), 10);
            const role = getUserRoleFromToken();

            if (isNaN(userId)) {
                setError("Can't identify user.");
                setLoading(false);
                return;
            }
            if (role !== '1') {
                navigate('/');
            }

            try {
                const response = await axios.get(`${API_BASE_URL}/api/admin/${userId}`, {
                    headers: getAuthHeaders(),
                });
                
                setUserInfo(response.data);
            } catch (error) {
                console.error("Error fetching user info:", error);
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchUserInfo();
    }, []);

    const handleLogout = () => {
        logout(navigate);
    };

    if (loading) return <div>Loading...</div>;
    if (error) return <div>{error}</div>;

    return (
        <>
            <div className="dashboard-container">
                <div className="header">
                    <h2>Admin Dashboard</h2>
                    <p>Welcome, {userInfo ? userInfo.username : 'Guest'}!</p>
                    <button onClick={handleLogout} className="dashboard-button logout-button">Logout</button>
                </div>
                
                <div className="dashboard-buttons">
                    <Link to="/admin/genre-list" className="dashboard-button">Manage Genres</Link>
                    <Link to="/admin/publisher-list" className="dashboard-button">Manage Publishers</Link>
                    <Link to="/admin/book-list" className="dashboard-button">Manage Books</Link>
                    <Link to="/admin/admin-list" className="dashboard-button">Manage Admin Accounts</Link>
                    <Link to="/admin/staff-list" className="dashboard-button">Manage Staff Accounts</Link>
                    <Link to="/admin/lessee-list" className="dashboard-button">Manage Lessee Accounts</Link>
                    <Link to="/admin/invoice-list" className="dashboard-button">Manage Invoices</Link>
                    <Link to="/admin/review-list" className="dashboard-button">Manage Reviews</Link>
                </div>
            </div>
        </>
        );
};

export default DashboardPage;