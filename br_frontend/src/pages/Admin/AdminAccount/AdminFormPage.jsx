import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getAuthHeaders } from '../../../services/userAuth';
import '../CSS/AdminForm.css';

const API_URL = 'http://localhost:5231/api/admin';

const AdminFormPage = () => {
    const [admin, setAdmin] = useState({
        username: '',
        password: '',
        fullName: '',
        email: '',
        role: '1',
    });
    const [isEdit, setIsEdit] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            setIsEdit(true);
            fetchAdmin(id);
        }
    }, [id]);

    const fetchAdmin = async (id) => {
        try {
            const response = await fetch(`${API_URL}/${id}`, {
                headers: getAuthHeaders(),
            });
            const data = await response.json();
            setAdmin(data);
        } catch (error) {
            console.error("Error fetching admin:", error);
            setError("Failed to fetch admin.");
        }
    };

    const handleChange = (e) => {
        setAdmin({ ...admin, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (isEdit) {
                await fetch(`${API_URL}/${id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                        ...getAuthHeaders(),
                    },
                    body: JSON.stringify(admin),
                });
            } else {
                await fetch(API_URL, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        ...getAuthHeaders(),
                    },
                    body: JSON.stringify(admin),
                });
            }
            navigate('/admin/admin-list');
        } catch (error) {
            console.error("Error saving admin:", error);
            setError("Failed to save admin.");
        }
    };

    return (
        <div className="admin-form-page-container">
            <h2>{isEdit ? 'Update Admin' : 'Add Admin'}</h2>
            {error && <div className="error-message">{error}</div>}
            <form onSubmit={handleSubmit}>
                <div className="input-group">
                    <label>Username:</label>
                    <input
                        type="text"
                        name="username"
                        value={admin.username}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="input-group">
                    <label>Password:</label>
                    <input
                        type="password"
                        name="password"
                        value={admin.password}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="input-group">
                    <label>Full Name:</label>
                    <input
                        type="text"
                        name="fullName"
                        value={admin.fullName}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="input-group">
                    <label>Email:</label>
                    <input
                        type="email"
                        name="email"
                        value={admin.email}
                        onChange={handleChange}
                        required
                    />
                </div>
                <button type="submit" className="submit-button">
                    {isEdit ? 'Update' : 'Add'}
                </button>
            </form>
        </div>
    );
};

export default AdminFormPage;