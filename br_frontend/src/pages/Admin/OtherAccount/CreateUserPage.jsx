import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getAuthHeaders } from '../../../services/userAuth';
import '../CSS/CreateUser.css';

const API_URL = 'http://localhost:5231/api/user';

const CreateUserPage = () => {
    const [user, setUser] = useState({
        username: '',
        password: '',
        fullName: '',
        address: '',
        email: '',
        phoneNumber: '',
        role: 0,
    });
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleChange = (e) => {
        setUser({ ...user, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const endpoint = user.role === 0 ? 'createLesse' : 'createStaff';
            const response = await fetch(`${API_URL}/${endpoint}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    ...getAuthHeaders(),
                },
                body: JSON.stringify(user),
            });

            if (!response.ok) {
                const errorData = await response.json();
                setError(errorData.message || 'Failed to create user.');
                return;
            }

            navigate('/admin/dashboard');
        } catch (error) {
            console.error("Error creating user:", error);
            setError("Failed to create user.");
        }
    };

    return (
        <div className="create-user-page-container">
            <h2>Create User</h2>
            {error && <div className="create-user-error-message">{error}</div>}
            <form onSubmit={handleSubmit}>
                <div className="create-user-input-group">
                    <label>Username:</label>
                    <input
                        type="text"
                        name="username"
                        value={user.username}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="create-user-input-group">
                    <label>Password:</label>
                    <input
                        type="password"
                        name="password"
                        value={user.password}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="create-user-input-group">
                    <label>Full Name:</label>
                    <input
                        type="text"
                        name="fullName"
                        value={user.fullName}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="create-user-input-group">
                    <label>Address:</label>
                    <input
                        type="text"
                        name="address"
                        value={user.address}
                        onChange={handleChange}
                    />
                </div>
                <div className="create-user-input-group">
                    <label>Email:</label>
                    <input
                        type="email"
                        name="email"
                        value={user.email}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="create-user-input-group">
                    <label>Phone Number:</label>
                    <input
                        type="text"
                        name="phoneNumber"
                        value={user.phoneNumber}
                        onChange={handleChange}
                    />
                </div>
                <div className="create-user-input-group">
                    <label>Role:</label>
                    <select name="role" value={user.role} onChange={handleChange}>
                        <option value={0}>Lesse</option>
                        <option value={2}>Staff</option>
                    </select>
                </div>
                <button type="submit" className="create-user-submit-button">Create User</button>
            </form>
        </div>
    );
};

export default CreateUserPage;