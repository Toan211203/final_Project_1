import React, { useEffect, useState } from 'react';
import { getUserIdFromToken, getUserRoleFromToken, getAuthHeaders } from '../../services/userAuth';
import axios from 'axios';
import '../../components/CSS/Account.css';
import Header from '../../components/Header';
import Sidebar from '../../components/Sidebar';

const API_BASE_URL = 'http://localhost:5231';

const Account = () => {
    const [userInfo, setUserInfo] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [formData, setFormData] = useState({
        fullName: '',
        username: '',
        email: '',
        address: '',
        phoneNumber: '',
        password: '',
        role: 0
    });

    useEffect(() => {
        const fetchUserInfo = async () => {
            const userId = parseInt(getUserIdFromToken(), 10);
            
            if (isNaN(userId)) {
                setError("Can't identify user.");
                setLoading(false);
                return;
            }

            const role = getUserRoleFromToken();
            try {
                let response;
                if (role === '0') {
                    response = await axios.get(`${API_BASE_URL}/api/Lesse/${userId}`, {
                        headers: getAuthHeaders(),
                    });
                } else if (role === '2') {
                    response = await axios.get(`${API_BASE_URL}/api/Staff/${userId}`, {
                        headers: getAuthHeaders(),
                    });
                } else {
                    setError("Role is not valid");
                    setLoading(false);
                    return;
                }

                setFormData({
                    fullName: response.data.fullName || '',
                    username: response.data.username || '',
                    email: response.data.email || '',
                    address: response.data.address || '',
                    phoneNumber: response.data.phoneNumber || '',
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

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const userId = parseInt(getUserIdFromToken(), 10);
            
        if (isNaN(userId)) {
            setError("Can't identify user.");
            return;
        }
        const role = getUserRoleFromToken();
        try {
            let response;
            if (role === '2') {
                response = await axios.put(`${API_BASE_URL}/api/staff/update/${userId}`, formData, {
                    headers: getAuthHeaders(),
                });
            } else if (role === '0') {
                response = await axios.put(`${API_BASE_URL}/api/lesse/update/${userId}`, formData, {
                    headers: getAuthHeaders(),
                });
            }

            setUserInfo(response.data);
            alert("Update info successfully!");
        } catch (error) {
            console.error("Error updating info:", error);
            setError("Cannot update info.");
        }
    };

    if (loading) return <div>Loading...</div>;
    if (error) return <div>{error}</div>;

    return (
        <>
            <Header/>
            <Sidebar/>
            <div className="account-container">
                <h2 className="account-title">User detail</h2>
                {userInfo ? (
                    <div className="account-info">
                        <form onSubmit={handleSubmit}>
                            <div className="account-input-group">
                                <label className="account-label">Full Name:</label>
                                <input type="text" className="account-input" name="fullName" value={formData.fullName} onChange={handleChange} />
                            </div>
                            <div className="account-input-group">
                                <label className="account-label">Username:</label>
                                <input type="text" className="account-input" name="username" value={formData.username} onChange={handleChange} />
                            </div>
                            <div className="account-input-group">
                                <label className="account-label">Email:</label>
                                <input type="email" className="account-input" name="email" value={formData.email} onChange={handleChange} />
                            </div>
                            <div className="account-input-group">
                                <label className="account-label">Address:</label>
                                <input type="text" className="account-input" name="address" value={formData.address} onChange={handleChange} />
                            </div>
                            <div className="account-input-group">
                                <label className="account-label">Phone Number:</label>
                                <input type="tel" className="account-input" name="phoneNumber" value={formData.phoneNumber} onChange={handleChange} />
                            </div>
                            <button type="submit" className="account-button">Update</button>
                        </form>
                    </div>
                ) : (
                    <p>There are no user detail.</p>
                )}
            </div>
        </>
    );
};

export default Account;