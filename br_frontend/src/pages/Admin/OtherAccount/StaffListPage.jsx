import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { getUserRoleFromToken } from '../../../services/userAuth';
import '../CSS/StaffList.css';

const StaffListPage = () => {
    const [staff, setStaff] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const role = getUserRoleFromToken();

        if (role !== '1') {
            navigate('/');
        } else {
            fetchStaff();
        }
    }, [navigate]);

    const fetchStaff = async () => {
        try {
            const response = await fetch('http://localhost:5231/api/staff/all');
            const data = await response.json();
            setStaff(data);
        } catch (error) {
            console.error("Error fetching staff:", error);
        }
    };

    return (
        <div className="staff-list-page-container">
            <h2>Staff List</h2>
            <Link to="/admin/create-user" className="staff-list-page-add-button">Add Staff</Link>
            <table className="staff-list-page-table">
                <thead>
                    <tr>
                        <th>Username</th>
                        <th>Full Name</th>
                        <th>Email</th>
                        <th>Phone Number</th>
                    </tr>
                </thead>
                <tbody>
                    {staff.map((member) => (
                        <tr key={member.userId}>
                            <td>{member.username}</td>
                            <td>{member.fullName}</td>
                            <td>{member.email}</td>
                            <td>{member.phoneNumber}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default StaffListPage;