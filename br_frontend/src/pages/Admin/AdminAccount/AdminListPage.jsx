import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { getUserRoleFromToken } from '../../../services/userAuth';
import '../CSS/AdminList.css';

const AdminListPage = () => {
    const [admins, setAdmins] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const role = getUserRoleFromToken();

        if (role !== '1') {
            navigate('/');
        } else {
            fetchAdmins();
        }
    }, [navigate]);

    const fetchAdmins = async () => {
        try {
            const response = await fetch('http://localhost:5231/api/admin/all');
            const data = await response.json();
            setAdmins(data);
        } catch (error) {
            console.error("Error fetching admins:", error);
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm("Are you sure you want to delete this admin? This action cannot be undone.")) {
            try {
                await fetch(`http://localhost:5231/api/admin/${id}`, {
                    method: 'DELETE',
                });
                fetchAdmins();
            } catch (error) {
                console.error("Error deleting admin:", error);
            }
        }
    };

    return (
        <div className="admin-list-page-container">
            <h2>Admin List</h2>
            <Link to="/admin/admin-form" className="admin-list-page-add-button">Add Admin</Link>
            <table className="admin-list-page-table">
                <thead>
                    <tr>
                        <th>Admin ID</th>
                        <th>Username</th>
                        <th>Email</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {admins.map((admin) => (
                        <tr key={admin.userId}>
                            <td>{admin.userId}</td>
                            <td>{admin.username}</td>
                            <td>{admin.email}</td>
                            <td>
                                <Link to={`/admin/admin-form/${admin.userId}`} className="admin-list-page-action-button detail">Detail</Link>
                                <button onClick={() => handleDelete(admin.userId)} className="admin-list-page-action-button delete">Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default AdminListPage;