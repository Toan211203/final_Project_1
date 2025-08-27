import React, { useState, useEffect } from 'react';
import { getUserIdFromToken, getUserRoleFromToken, getAuthHeaders } from '../../services/userAuth';
import axios from 'axios';
//import '../../components/CSS/ChangePassword.css';
import Header from '../../components/Header';
import Sidebar from '../../components/Sidebar';

const API_BASE_URL = 'http://localhost:5231';

const ChangePassword = () => {
    const [oldPassword, setOldPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(null);
    const [userInfo, setUserInfo] = useState(null);
    const [role, setRole] = useState(null);

    useEffect(() => {
        const fetchUserInfo = async () => {
            const userId = parseInt(getUserIdFromToken(), 10);
            const userRole = getUserRoleFromToken();

            if (isNaN(userId)) {
                setError("Can't identify user.");
                return;
            }

            setRole(userRole);

            try {
                const response = await axios.get(`${API_BASE_URL}/api/lesse/${userId}`, {
                    headers: getAuthHeaders(),
                });
                setUserInfo(response.data);
            } catch (error) {
                console.error("Error fetching user info:", error);
                setError("Failed to fetch user info.");
            }
        };

        fetchUserInfo();
    }, []);

    const handleChange = (e) => {
        const { name, value } = e.target;
        if (name === 'oldPassword') setOldPassword(value);
        else if (name === 'newPassword') setNewPassword(value);
        else if (name === 'confirmPassword') setConfirmPassword(value);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const userId = parseInt(getUserIdFromToken(), 10);

        if (isNaN(userId)) {
            setError("Can't identify user.");
            return;
        }

        if (newPassword !== confirmPassword) {
            setError("New password and confirmation do not match.");
            return;
        }

        try {
            let response;
            const updatedData = {
                Username: userInfo.Username,
                FullName: userInfo.FullName,
                Address: userInfo.Address,
                Email: userInfo.Email,
                PhoneNumber: userInfo.PhoneNumber,
                Password: newPassword,
                Role: userInfo.Role,
            };

            if (role === '0') {
                response = await axios.put(`${API_BASE_URL}/api/lesse/update/${userId}`, updatedData, {
                    headers: getAuthHeaders(),
                });
            } else if (role === '2') {
                response = await axios.put(`${API_BASE_URL}/api/staff/update/${userId}`, updatedData, {
                    headers: getAuthHeaders(),
                });
            } else {
                setError("You do not have permission to change the password.");
                return;
            }

            if (response.data) {
                setSuccess("Password updated successfully!");
                setOldPassword('');
                setNewPassword('');
                setConfirmPassword('');
            } else {
                setError("Failed to update password.");
            }
        } catch (error) {
            console.error("Error changing password:", error);
            setError("An error occurred while changing the password.");
        }
    };

    return (
        <>
            <Header />
            <Sidebar />
            <div className="change-password-container">
                <h2 className="change-password-title">Change Password</h2>
                {error && <div className="error-message">{error}</div>}
                {success && <div className="success-message">{success}</div>}
                <form onSubmit={handleSubmit}>
                    <div className="input-group">
                        <label className="input-label">Old Password:</label>
                        <input type="password" className="input" name="oldPassword" value={oldPassword} onChange={handleChange} required />
                    </div>
                    <div className="input-group">
                        <label className="input-label">New Password:</label>
                        <input type="password" className="input" name="newPassword" value={newPassword} onChange={handleChange} required />
                    </div>
                    <div className="input-group">
                        <label className="input-label">Confirm New Password:</label>
                        <input type="password" className="input" name="confirmPassword" value={confirmPassword} onChange={handleChange} required />
                    </div>
                    <button type="submit" className="submit-button">Change Password</button>
                </form>
            </div>
        </>
    );
};

export default ChangePassword;